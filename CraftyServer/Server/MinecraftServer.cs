using System;
using CraftyServer.Core;
using ikvm.extensions;
using IKVM.Runtime;
using java.awt;
using java.io;
using java.lang;
using java.net;
using java.util;
using java.util.logging;
using Exception = System.Exception;
using List = java.util.List;
using String = java.lang.String;

namespace CraftyServer.Server
{
    public class MinecraftServer : Runnable, ICommandListener
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        public static HashMap field_6037_b = new HashMap();
        private readonly List commands;
        private readonly List field_9010_p;
        private ConsoleCommandHandler commandHandler;
        public ServerConfigurationManager configManager;
        public string currentTask;
        private int deathTime;
        public EntityTracker entityTracker;
        public NetworkListenThread networkServer;
        public bool onlineMode;
        public int percentDone;
        public PropertyManager propertyManagerObj;
        public bool pvpOn;
        public bool serverRunning;
        public bool serverStopped;
        public bool spawnPeacefulMobs;
        public WorldServer worldMngr;

        public MinecraftServer()
        {
            serverRunning = true;
            serverStopped = false;
            deathTime = 0;
            field_9010_p = new ArrayList();
            commands = Collections.synchronizedList(new ArrayList());
            new ThreadSleepForever(this);
        }

        #region ICommandListener Members

        public void log(string s)
        {
            logger.info(s);
        }

        public string getUsername()
        {
            return "CONSOLE";
        }

        #endregion

        #region Runnable Members

        public void run()
        {
            try
            {
                if (startServer())
                {
                    long l = java.lang.System.currentTimeMillis();
                    long l1 = 0L;
                    while (serverRunning)
                    {
                        long l2 = java.lang.System.currentTimeMillis();
                        long l3 = l2 - l;
                        if (l3 > 2000L)
                        {
                            logger.warning("Can't keep up! Did the system time change, or is the server overloaded?");
                            l3 = 2000L;
                        }
                        if (l3 < 0L)
                        {
                            logger.warning("Time ran backwards! Did the system time change?");
                            l3 = 0L;
                        }
                        l1 += l3;
                        l = l2;
                        if (worldMngr.isAllPlayersFullyAsleep())
                        {
                            doTick();
                            l1 = 0L;
                        }
                        else
                        {
                            while (l1 > 50L)
                            {
                                l1 -= 50L;
                                doTick();
                            }
                        }
                        Thread.sleep(1L);
                    }
                }
                else
                {
                    while (serverRunning)
                    {
                        commandLineParser();
                        try
                        {
                            Thread.sleep(10L);
                        }
                        catch (InterruptedException interruptedexception)
                        {
                            interruptedexception.printStackTrace();
                        }
                    }
                }
            }
            catch (Exception throwable1)
            {
                throwable1.printStackTrace();
                logger.log(Level.SEVERE, "Unexpected exception", throwable1);
                while (serverRunning)
                {
                    commandLineParser();
                    try
                    {
                        Thread.sleep(10L);
                    }
                    catch (InterruptedException interruptedexception1)
                    {
                        interruptedexception1.printStackTrace();
                    }
                }
            }
            finally
            {
                try
                {
                    stopServer();
                    serverStopped = true;
                }
                catch (Exception localThrowable4)
                {
                    localThrowable4.printStackTrace();
                }
                finally
                {
                    // System.Console.ReadLine();
                    java.lang.System.exit(0);
                }
            }
        }

        #endregion

        private bool startServer()
        {
            commandHandler = new ConsoleCommandHandler(this);
            var threadcommandreader = new ThreadCommandReader(this);
            threadcommandreader.setDaemon(true);
            threadcommandreader.start();
            ConsoleLogManager.init();
            logger.info("Starting Crafty version " + Crafty.VERSION + "(based on minecraft server version Beta 1.3)");

            logger.info("Loading properties");
            propertyManagerObj = new PropertyManager(new File("server.properties"));
            string s = propertyManagerObj.getStringProperty("server-ip", "");
            onlineMode = propertyManagerObj.getBooleanProperty("online-mode", true);
            spawnPeacefulMobs = propertyManagerObj.getBooleanProperty("spawn-animals", true);
            pvpOn = propertyManagerObj.getBooleanProperty("pvp", true);
            InetAddress inetaddress = null;
            if (s.Length > 0)
            {
                inetaddress = InetAddress.getByName(s);
            }
            int i = propertyManagerObj.getIntProperty("server-port", 25565);
            logger.info(
                (new StringBuilder()).append("Starting Minecraft server on ").append(s.Length != 0 ? s : "*").append(":")
                    .append(i).toString());
            networkServer = new NetworkListenThread(this, inetaddress, i);
            if (!onlineMode)
            {
                logger.warning("**** SERVER IS RUNNING IN OFFLINE/INSECURE MODE!");
                logger.warning("The server will make no attempt to authenticate usernames. Beware.");
                logger.warning(
                    "While this makes the game possible to play without internet access, it also opens up the ability for hackers to connect with any username they choose.");
                logger.warning("To change this, set \"online-mode\" to \"true\" in the server.settings file.");
            }
            configManager = new ServerConfigurationManager(this);
            entityTracker = new EntityTracker(this);
            long l = java.lang.System.nanoTime();
            string s1 = propertyManagerObj.getStringProperty("level-name", "world");
            logger.info((new StringBuilder()).append("Preparing level \"").append(s1).append("\"").toString());
            initWorld(new SaveConverterMcRegion(new File(".")), s1);
            logger.info(
                (new StringBuilder()).append("Done (").append(java.lang.System.nanoTime() - l).append(
                    "ns)! For help, type \"help\" or \"?\"").toString());
            return true;
        }

        private void initWorld(ISaveFormat isaveformat, string s)
        {
            if (isaveformat.func_22102_a(s))
            {
                logger.info("Converting map!");
                isaveformat.func_22101_a(s, new ConvertProgressUpdater(this));
            }
            logger.info("Preparing start region");
            worldMngr = new WorldServer(this, new SaveOldDir(new File("."), s, true), s,
                                        propertyManagerObj.getBooleanProperty("hellworld", false) ? -1 : 0);
            worldMngr.addWorldAccess(new WorldManager(this));
            worldMngr.difficultySetting = propertyManagerObj.getBooleanProperty("spawn-monsters", true) ? 1 : 0;
            worldMngr.setAllowedSpawnTypes(propertyManagerObj.getBooleanProperty("spawn-monsters", true),
                                           spawnPeacefulMobs);
            configManager.setPlayerManager(worldMngr);
            char c = '\x00C4'; // \304
            long l = java.lang.System.currentTimeMillis();
            ChunkCoordinates chunkcoordinates = worldMngr.func_22078_l();
            for (int i = -c; i <= c && serverRunning; i += 16)
            {
                for (int j = -c; j <= c && serverRunning; j += 16)
                {
                    long l1 = java.lang.System.currentTimeMillis();
                    if (l1 < l)
                    {
                        l = l1;
                    }
                    if (l1 > l + 1000L)
                    {
                        int k = (c*2 + 1)*(c*2 + 1);
                        int i1 = (i + c)*(c*2 + 1) + (j + 1);
                        outputPercentRemaining("Preparing spawn area", (i1*100)/k);
                        l = l1;
                    }
                    worldMngr.field_20911_y.loadChunk(chunkcoordinates.posX + i >> 4, chunkcoordinates.posZ + j >> 4);
                    while (worldMngr.func_6156_d() && serverRunning) ;
                }
            }

            clearCurrentTask();
        }

        private void outputPercentRemaining(string s, int i)
        {
            currentTask = s;
            percentDone = i;
            logger.info((new StringBuilder()).append(s).append(": ").append(i).append("%").toString());
        }

        private void clearCurrentTask()
        {
            currentTask = null;
            percentDone = 0;
        }

        private void saveServerWorld()
        {
            logger.info("Saving chunks");
            worldMngr.saveWorld(true, null);
            worldMngr.func_22088_r();
        }

        private void stopServer()
        {
            logger.info("Stopping server");
            if (configManager != null)
            {
                configManager.savePlayerStates();
            }
            if (worldMngr != null)
            {
                saveServerWorld();
            }
        }

        public void initiateShutdown()
        {
            serverRunning = false;
        }

        private void doTick()
        {
            var arraylist = new ArrayList();
            for (Iterator iterator = field_6037_b.keySet().iterator(); iterator.hasNext();)
            {
                var s = (string) iterator.next();
                int k = ((Integer) field_6037_b.get(s)).intValue();
                if (k > 0)
                {
                    field_6037_b.put(s, Integer.valueOf(k - 1));
                }
                else
                {
                    arraylist.add(s);
                }
            }

            for (int i = 0; i < arraylist.size(); i++)
            {
                field_6037_b.remove(arraylist.get(i));
            }

            AxisAlignedBB.clearBoundingBoxPool();
            Vec3D.initialize();
            deathTime++;
            if (deathTime%20 == 0)
            {
                configManager.sendPacketToAllPlayers(new Packet4UpdateTime(worldMngr.getWorldTime()));
            }
            worldMngr.tick();
            while (worldMngr.func_6156_d()) ;
            worldMngr.updateEntities();
            networkServer.func_715_a();
            configManager.func_637_b();
            entityTracker.updateTrackedEntities();
            for (int j = 0; j < field_9010_p.size(); j++)
            {
                ((IUpdatePlayerListBox) field_9010_p.get(j)).update();
            }

            try
            {
                commandLineParser();
            }
            catch (java.lang.Exception exception)
            {
                logger.log(Level.WARNING, "Unexpected exception while parsing console command", exception);
            }
        }

        public void addCommand(string s, ICommandListener icommandlistener)
        {
            commands.add(new ServerCommand(s, icommandlistener));
        }

        public void commandLineParser()
        {
            ServerCommand servercommand;
            for (; commands.size() > 0; commandHandler.handleCommand(servercommand))
            {
                servercommand = (ServerCommand) commands.remove(0);
            }
        }

        public void func_6022_a(IUpdatePlayerListBox iupdateplayerlistbox)
        {
            field_9010_p.add(iupdateplayerlistbox);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                var minecraftserver = new MinecraftServer();
                if (!GraphicsEnvironment.isHeadless() &&
                    ((args.Length <= 0) || !String.instancehelper_equals(args[0], "nogui")))
                {
                    ServerGUI.initGui(minecraftserver);
                }
                new ThreadServerApplication("Server thread", minecraftserver).start();
            }
            catch (java.lang.Exception exception1)
            {
                var local1 = ByteCodeHelper.MapException<java.lang.Exception>(exception1, ByteCodeHelper.MapFlags.None);
                if (local1 == null)
                {
                    throw;
                }
                java.lang.Exception exception = local1;
                java.lang.Exception exception2 = exception;
                logger.log(Level.SEVERE, "Failed to start the minecraft server", exception2);
                return;
            }
        }

        public File getFile(string s)
        {
            return new File(s);
        }

        public static bool isServerRunning(MinecraftServer minecraftserver)
        {
            return minecraftserver.serverRunning;
        }
    }
}