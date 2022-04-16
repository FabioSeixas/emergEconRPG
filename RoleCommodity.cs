namespace econrpg
{
    public class RoleCommodity
    {
        Commodity commodity;
        int threshold;
        bool produced;
        public RoleCommodity(Commodity commodity, int threshold, bool produced)
        {
            this.commodity = commodity;
            this.threshold = threshold;
            this.produced = produced;
        }

        public int getThreshold()
        {
            return this.threshold;
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