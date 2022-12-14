using System.Collections.Generic;

namespace CardLearnApp.Data
{
    [System.Serializable]
    public class MainDataContainer
    {
        private List<CardsBundleContainer> bundles;
        public List<CardsBundleContainer> Bundles { get => bundles; set => bundles = value; }


        private static MainDataContainer instance;

        public static MainDataContainer Initialize()
        {
            if (instance == null) instance = new MainDataContainer();

            return instance;
        }

        private MainDataContainer()
        {
            bundles = new List<CardsBundleContainer>();
        }
    }
}