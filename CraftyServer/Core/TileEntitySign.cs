using java.lang;

namespace CraftyServer.Core
{
    public class TileEntitySign : TileEntity
    {
        public TileEntitySign()
        {
            lineBeingEdited = -1;
        }

        public override void writeToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeToNBT(nbttagcompound);
            nbttagcompound.setString("Text1", signText[0]);
            nbttagcompound.setString("Text2", signText[1]);
            nbttagcompound.setString("Text3", signText[2]);
            nbttagcompound.setString("Text4", signText[3]);
        }

        public override void readFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readFromNBT(nbttagcompound);
            for (int i = 0; i < 4; i++)
            {
                signText[i] = nbttagcompound.getString((new StringBuilder()).append("Text").append(i + 1).toString());
                if (signText[i].Length > 15)
                {
                    signText[i] = signText[i].Substring(0, 15);
                }
            }
        }

        public override Packet getDescriptionPacket()
        {
            string[] ask = new string[4];
            for (int i = 0; i < 4; i++)
            {
                ask[i] = signText[i];
            }

            return new Packet130(xCoord, yCoord, zCoord, ask);
        }

        public string[] signText = {
                                       "", "", "", ""
                                   };

        public int lineBeingEdited;
    }
}