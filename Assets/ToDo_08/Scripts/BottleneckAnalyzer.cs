using System.Collections.Generic;
using AlgoLesson8;

namespace AlgoLesson8.Student
{
    /// <summary>
    /// 활성 기계 중 분당 처리량이 가장 낮은 병목 기계를 찾습니다.
    /// </summary>
    public sealed class BottleneckAnalyzer : IBottleneckAnalyzer
    {
        public string FindBottleneck(IReadOnlyList<MachineSnapshot> machines)
        {
            if (machines == null || machines.Count == 0)
                return string.Empty;

            MachineSnapshot best = null;

            foreach (MachineSnapshot machine in machines)
            {

                if(!machine.Enabled) continue;

                // 1. 아직 후보가 없다. (첫 번째 가동 기계)
                // 2. 현재 기계가 후보보다 더 느리다. (처리량이 더 작다)
                // 3. 처리량이 같다면 ID가 사전순으로 더 앞선다.
                if(best == null ||
                    machine.ItemsPerMinute < best.ItemsPerMinute ||
                    (machine.ItemsPerMinute == best.ItemsPerMinute && string.CompareOrdinal(machine.Id, best.Id) < 0))
                {
                    best = machine;
                }

                return best == null ? string.Empty : best.DisplayName;
            }

            // TODO 13: 비활성 기계는 검사에서 제외하세요.

            // TODO 14: ItemsPerMinute가 가장 낮은 기계를 찾으세요.

            // TODO 15: 처리량이 같으면 Id가 작은 기계를 선택하세요.

            // TODO 16: 선택된 기계의 DisplayName을 반환하세요.

            return string.Empty;
        }
    }
}
