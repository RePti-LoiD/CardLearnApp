using System.Collections.Generic;

namespace CardLearnApp.Data
{
    [System.Serializable]
    public class MainDataContainer
    {
        private List<BundleContainer> bundles;
        public List<BundleContainer> Bundles { get => bundles; set => bundles = value; }


        private static MainDataContainer instance;

        public static MainDataContainer Initialize()
        {
            if (instance == null) instance = new MainDataContainer();

            return instance;
        }

        private MainDataContainer()
        {
            bundles = new List<BundleContainer>();
        }
    }
}