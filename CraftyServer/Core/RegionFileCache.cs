using java.io;
using java.lang.@ref;
using java.util;
using java.lang;

//using java.lang.ref.Reference;
//using java.lang.ref.SoftReference;

namespace CraftyServer.Core
{
    public class RegionFileCache
    {
        private RegionFileCache()
        {
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public static RegionFile func_22123_a(File file, int i, int j)
        {
            File file1 = new File(file, "region");
            File file2 = new File(file1,
                                  (new StringBuilder()).append("r.").append(i >> 5).append(".").append(j >> 5).append(
                                      ".mcr").toString());
            java.lang.@ref.Reference reference = (Reference) field_22125_a.get(file2);
            if (reference != null)
            {
                RegionFile regionfile = (RegionFile) reference.get();
                if (regionfile != null)
                {
                    return regionfile;
                }
            }
            if (!file1.exists())
            {
                file1.mkdirs();
            }
            if (field_22125_a.size() >= 256)
            {
                func_22122_a();
            }
            RegionFile regionfile1 = new RegionFile(file2);
            field_22125_a.put(file2, new java.lang.@ref.SoftReference(regionfile1));
            return regionfile1;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public static void func_22122_a()
        {
            Iterator iterator = field_22125_a.values().iterator();
            do
            {
                if (!iterator.hasNext())
                {
                    break;
                }
                Reference reference = (java.lang.@ref.Reference) iterator.next();
                try
                {
                    RegionFile regionfile = (RegionFile) reference.get();
                    if (regionfile != null)
                    {
                        regionfile.close();
                    }
                }
                catch (IOException ioexception)
                {
                    ioexception.printStackTrace();
                }
            } while (true);
            field_22125_a.clear();
        }

        public static int func_22121_b(File file, int i, int j)
        {
            RegionFile regionfile = func_22123_a(file, i, j);
            return regionfile.getSizeDelta();
        }

        public static DataInputStream func_22124_c(File file, int i, int j)
        {
            RegionFile regionfile = func_22123_a(file, i, j);
            return regionfile.getChunkDataInputStream(i & 0x1f, j & 0x1f);
        }

        public static DataOutputStream func_22120_d(File file, int i, int j)
        {
            RegionFile regionfile = func_22123_a(file, i, j);
            return regionfile.getChunkDataOutputStream(i & 0x1f, j & 0x1f);
        }

        private static Map field_22125_a = new HashMap();
    }
}