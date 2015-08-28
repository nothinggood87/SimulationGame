﻿using System;
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
            BaseMass = tileBaseMass;
        }
        //base values
        public bool IsPlayer = false;
        private double Rotation { get; set; } = 0;
        private double[] MicroVelocity { get; set; } = new double[2];
        private short[] FlashVelocity { get; set; } = new short[2];
        public int ClusterId { get; set; } = -1;
        public bool HasMoved { get; set; } = false;
        public int BaseMass { get; set; } = 0;
        public int pressure { get; set; } = 1;
        public double[] velocity { get; set; }
        //other values
        public int mass => BaseMass * pressure;
        public int[] velocityRoundedActual => FlashVelocityRounded();
        private void MicroVelocityUpdate()
        {
            int[] velocityRounded = Helper.GetRoundedVelocity(velocity);
            MicroVelocity[0] += velocity[0];
            MicroVelocity[1] += velocity[1];
            MicroVelocity[0] -= velocityRounded[0];
            MicroVelocity[1] -= velocityRounded[1];
            if (MicroVelocity[0] >= 1) { FlashVelocity[0]++;MicroVelocity[0]--; }
            if (MicroVelocity[0] <= -1) { FlashVelocity[0]--; MicroVelocity[0]++; }
            if (MicroVelocity[1] >= 1) { FlashVelocity[1]++; MicroVelocity[1]--; }
            if (MicroVelocity[1] <= -1) { FlashVelocity[1]--; MicroVelocity[1]++; }
        }
        private int[] FlashVelocityRounded()
        {
            int[] velocity = Helper.GetRoundedVelocity(this.velocity);
            velocity[0] += FlashVelocity[0];
            velocity[0] += FlashVelocity[0];
            return velocity;
        }
        public void InternalTick()
        {
            if (FlashVelocity[0] != 0) { FlashVelocity[0] = 0; }
            if (FlashVelocity[1] != 0) { FlashVelocity[1] = 0; }
            MicroVelocityUpdate();
        }
        public void SetTile(Tile tile)
        {
            IsPlayer = tile.IsPlayer;
            Rotation = tile.Rotation;
            MicroVelocity = tile.MicroVelocity;
            FlashVelocity = tile.FlashVelocity;
            ClusterId = tile.ClusterId;
            HasMoved = tile.HasMoved;
            BaseMass = tile.BaseMass;
            pressure = tile.pressure;
            velocity = tile.velocity;
        }
    }
}
