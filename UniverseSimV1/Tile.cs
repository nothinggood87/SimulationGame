using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    class Tile
    {
        public Tile()
            :this(0)
        { }
        public Tile(int tileBaseMass)
            :this(tileBaseMass,new double[2])
        { }
        public Tile(int tileBaseMass,double[] tileVelocity)
        {
            velocity = tileVelocity;
            baseMass = tileBaseMass;
        }
        //base values
        public bool isPlayer = false;
        private double rotation { get; set; } = 0;
        private double[] microVelocity { get; set; } = new double[2];
        private short[] flashVelocity { get; set; } = new short[2];
        public int clusterId { get; set; } = -1;
        public bool hasMoved { get; set; } = false;
        public int baseMass { get; set; } = 0;
        public int pressure { get; set; } = 1;
        public double[] velocity { get; set; }
        //other values
        public int mass => baseMass * pressure;
        public int[] velocityRoundedActual => FlashVelocityRounded();
        private void MicroVelocityUpdate()
        {
            int[] velocityMatrixRounded = Helper.GetRoundedVelocity(velocity);
            microVelocity[0] += velocity[0];
            microVelocity[1] += velocity[1];
            microVelocity[0] -= velocityMatrixRounded[0];
            microVelocity[1] -= velocityMatrixRounded[1];
            if (microVelocity[0] >= 1) { flashVelocity[0]++;microVelocity[0]--; }
            if (microVelocity[0] <= -1) { flashVelocity[0]--; microVelocity[0]++; }
            if (microVelocity[1] >= 1) { flashVelocity[1]++; microVelocity[1]--; }
            if (microVelocity[1] <= -1) { flashVelocity[1]--; microVelocity[1]++; }
        }
        private int[] FlashVelocityRounded()
        {
            int[] velocity = Helper.GetRoundedVelocity(this.velocity);
            velocity[0] += flashVelocity[0];
            velocity[0] += flashVelocity[0];
            return velocity;
        }
        public void InternalTick()
        {
            if (flashVelocity[0] != 0) { flashVelocity[0] = 0; }
            if (flashVelocity[1] != 0) { flashVelocity[1] = 0; }
            MicroVelocityUpdate();
        }
        public void SetTile(Tile tile)
        {
            isPlayer = tile.isPlayer;
            rotation = tile.rotation;
            microVelocity = tile.microVelocity;
            flashVelocity = tile.flashVelocity;
            clusterId = tile.clusterId;
            hasMoved = tile.hasMoved;
            baseMass = tile.baseMass;
            pressure = tile.pressure;
            velocity = tile.velocity;
        }
    }
}
