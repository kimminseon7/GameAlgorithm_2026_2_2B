using System.Collections.Generic;
using AlgoLesson8;

namespace AlgoLesson8.Student
{
    /// <summary>
    /// 긴급도, 남은 시간, 보상 순서로 다음 주문을 선택합니다.
    /// </summary>
    public sealed class OrderScheduler : IOrderScheduler
    {
        public int SelectNextOrder(IReadOnlyList<WorkOrder> orders, float currentTime)
        {
            if (orders == null || orders.Count == 0)
                return -1;

            WorkOrder best = orders[0];

            for (int i = 1; i < orders.Count; i++)
            {
                WorkOrder candidate = orders[i];

                bool better =
                    candidate.Priority > best.Priority ||
                    (candidate.Priority == best.Priority && candidate.Deadline < best.Deadline) ||
                    (candidate.Priority == best.Priority && candidate.Deadline == best.Deadline && candidate.Reward > best.Reward) ||
                    (candidate.Priority == best.Priority && candidate.Deadline == best.Deadline && candidate.Reward > best.Reward && candidate.Id < best.Id);

                if (better) best = candidate;
            }

            // TODO 08: bestOrder를 첫 주문으로 초기화하세요.

            // TODO 09: Priority가 높은 주문을 우선하세요.

            // TODO 10: Priority가 같으면 Deadline이 짧은 주문을 우선하세요.

            // TODO 11: 그래도 같으면 Reward가 높은 주문을 우선하세요.

            // TODO 12: 마지막 동률은 Id가 작은 주문을 선택하세요.

            return best.Id;
        }
    }
}
