using System.Collections.Generic;
using AlgoCourse.Lesson3;

namespace AlgoCourse.StudentWork
{
    public sealed class PandemicInfectionAlgorithm : ICityInfectionAlgorithm
    {
        private const int MaximumInfectionLevel = 3;
        private readonly Dictionary<int, int[]> graph = new Dictionary<int, int[]>();
        private readonly Dictionary<int, int> infectionLevels = new Dictionary<int, int>();
        private readonly Queue<int> infectionQueue = new Queue<int>();
        private readonly HashSet<int> outbreakCities = new HashSet<int>();

        public int PendingCount => infectionQueue.Count;
        public int OutbreakCount { get; private set; }

        public void Initialize(IReadOnlyDictionary<int, int[]> cityGraph)
        {
            graph.Clear();
            infectionLevels.Clear();
            infectionQueue.Clear();
            outbreakCities.Clear();

            OutbreakCount = 0;

            foreach(KeyValuePair<int, int[]> city in cityGraph)
            {
                graph.Add(city.Key, city.Value);
                infectionLevels.Add(city.Key, 0);
            }
        }

        public bool QueueInfection(int cityId)
        {
            if(!graph.ContainsKey(cityId))
            {
                return false;
            }

            if (infectionQueue.Count == 0)
            {
                outbreakCities.Clear();
            }

            infectionQueue.Enqueue(cityId);

            return true;
        }

        public CityInfectionStep ProcessNext()
        {
            if (infectionQueue.Count == 0)
            {
                return new CityInfectionStep(-1, 0, false, false);
            }

            int cityld = infectionQueue.Dequeue();
            int currentLevel = infectionLevels[cityld];

            if (currentLevel < MaximumInfectionLevel)
            {
                int nextLevel = currentLevel + 1;
                infectionLevels[cityld] = nextLevel;
                return new CityInfectionStep(cityld, nextLevel, false, true);
            }

            if (!outbreakCities.Add(cityld))
            {
                return new CityInfectionStep(cityld, currentLevel, false, false);
            }

            OutbreakCount++;

            foreach (int neighborld in graph[cityld])
            {
                infectionQueue.Enqueue(neighborld);
            }

            return new CityInfectionStep(cityld, currentLevel, true, true);
        }

        public int GetInfectionLevel(int cityId)
        {
            return infectionLevels.TryGetValue(cityId, out int level) ? level : 0;
        }
    }
}
