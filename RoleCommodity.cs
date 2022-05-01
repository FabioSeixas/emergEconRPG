namespace econrpg
{
    public class RoleCommodity
    {
        Commodity commodity;
        int fullThreshold;
        int notFullThreshold;
        bool produced;
        public RoleCommodity(Commodity commodity, int fullThreshold, int notFullThreshold, bool produced)
        {
            this.commodity = commodity;
            this.fullThreshold = fullThreshold;
            this.notFullThreshold = notFullThreshold;
            this.produced = produced;
        }

        public int getThreshold(bool full = true)
        {
            return full ? this.fullThreshold : this.notFullThreshold;
        }
        public Commodity GetCommodity()
        {
            return this.commodity;
        }
        public bool isProduced()
        {
            return this.produced;
        }
        public int getCommodityId()
        {
            return this.commodity.getId();
        }
    }
}