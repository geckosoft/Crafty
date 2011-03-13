using java.io;
using java.util;
using java.util.zip;
using java.lang;

namespace CraftyServer.Core
{
    public class SaveConverterMcRegion : SaveFormatOld
    {
        public SaveConverterMcRegion(File file)
            : base(file)
        {
        }

        public override ISaveHandler func_22105_a(string s, bool flag)
        {
            return new SaveOldDir(field_22106_a, s, flag);
        }

        public override bool func_22102_a(string s)
        {
            WorldInfo worldinfo = func_22103_b(s);
            return worldinfo != null && worldinfo.func_22188_i() == 0;
        }

        public override bool func_22101_a(string s, IProgressUpdate iprogressupdate)
        {
            iprogressupdate.setLoadingProgress(0);
            ArrayList arraylist = new ArrayList();
            ArrayList arraylist1 = new ArrayList();
            ArrayList arraylist2 = new ArrayList();
            ArrayList arraylist3 = new ArrayList();
            File file = new File(field_22106_a, s);
            File file1 = new File(file, "DIM-1");
            java.lang.System.@out.println("Scanning folders...");
            func_22108_a(file, arraylist, arraylist1);
            if (file1.exists())
            {
                func_22108_a(file1, arraylist2, arraylist3);
            }
            int i = arraylist.size() + arraylist2.size() + arraylist1.size() + arraylist3.size();
            java.lang.System.@out.println(
                (new StringBuilder()).append("Total conversion count is ").append(i).toString());
            func_22107_a(file, arraylist, 0, i, iprogressupdate);
            func_22107_a(file1, arraylist2, arraylist.size(), i, iprogressupdate);
            WorldInfo worldinfo = func_22103_b(s);
            worldinfo.func_22191_a(19132);
            ISaveHandler isavehandler = func_22105_a(s, false);
            isavehandler.func_22094_a(worldinfo);
            func_22109_a(arraylist1, arraylist.size() + arraylist2.size(), i, iprogressupdate);
            if (file1.exists())
            {
                func_22109_a(arraylist3, arraylist.size() + arraylist2.size() + arraylist1.size(), i, iprogressupdate);
            }
            return true;
        }

        private void func_22108_a(File file, ArrayList arraylist, ArrayList arraylist1)
        {
            ChunkFolderPattern chunkfolderpattern = new ChunkFolderPattern(null);
            ChunkFilePattern chunkfilepattern = new ChunkFilePattern(null);
            File[] afile = file.listFiles(chunkfolderpattern);
            File[] afile1 = afile;
            int i = afile1.Length;
            for (int j = 0; j < i; j++)
            {
                File file1 = afile1[j];
                arraylist1.add(file1);
                File[] afile2 = file1.listFiles(chunkfolderpattern);
                File[] afile3 = afile2;
                int k = afile3.Length;
                for (int l = 0; l < k; l++)
                {
                    File file2 = afile3[l];
                    File[] afile4 = file2.listFiles(chunkfilepattern);
                    File[] afile5 = afile4;
                    int i1 = afile5.Length;
                    for (int j1 = 0; j1 < i1; j1++)
                    {
                        File file3 = afile5[j1];
                        arraylist.add(new FileMatcher(file3));
                    }
                }
            }
        }

        private void func_22107_a(File file, ArrayList arraylist, int i, int j, IProgressUpdate iprogressupdate)
        {
            Collections.sort(arraylist);
            byte[] abyte0 = new byte[4096];
            int i1;
            for (Iterator iterator = arraylist.iterator(); iterator.hasNext(); iprogressupdate.setLoadingProgress(i1))
            {
                FileMatcher filematcher = (FileMatcher) iterator.next();
                int k = filematcher.func_22205_b();
                int l = filematcher.func_22204_c();
                RegionFile regionfile = RegionFileCache.func_22123_a(file, k, l);
                if (!regionfile.isChunkSaved(k & 0x1f, l & 0x1f))
                {
                    try
                    {
                        DataInputStream datainputstream =
                            new DataInputStream(new GZIPInputStream(new FileInputStream(filematcher.func_22207_a())));
                        DataOutputStream dataoutputstream = regionfile.getChunkDataOutputStream(k & 0x1f, l & 0x1f);
                        for (int j1 = 0; (j1 = datainputstream.read(abyte0)) != -1;)
                        {
                            dataoutputstream.write(abyte0, 0, j1);
                        }

                        dataoutputstream.close();
                        datainputstream.close();
                    }
                    catch (IOException ioexception)
                    {
                        ioexception.printStackTrace();
                    }
                }
                i++;
                i1 = (int) Math.round((100D*(double) i)/(double) j);
            }

            RegionFileCache.func_22122_a();
        }

        private void func_22109_a(ArrayList arraylist, int i, int j, IProgressUpdate iprogressupdate)
        {
            int k;
            for (Iterator iterator = arraylist.iterator(); iterator.hasNext(); iprogressupdate.setLoadingProgress(k))
            {
                File file = (File) iterator.next();
                File[] afile = file.listFiles();
                func_22104_a(afile);
                file.delete();
                i++;
                k = (int) Math.round((100D*(double) i)/(double) j);
            }
        }
    }
}