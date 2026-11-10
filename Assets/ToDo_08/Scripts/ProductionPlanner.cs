using System.Collections.Generic;
using AlgoLesson8;

namespace AlgoLesson8.Student
{
    /// <summary>
    /// 제작법 그래프를 탐색해 재료부터 완제품까지의 생산 순서를 만듭니다.
    /// </summary>
    public sealed class ProductionPlanner : IProductionPlanner
    {
        public ProductionPlan BuildPlan(
            string targetProduct,
            IReadOnlyList<RecipeDefinition> recipes)
        {
            ProductionPlan result = new ProductionPlan();

            Dictionary<string, RecipeDefinition> recipeMap = new Dictionary<string, RecipeDefinition>();

            foreach (RecipeDefinition recipe in recipes)
            {
                recipeMap[recipe.Output] = recipe;
            }

            HashSet<string> visited = new HashSet<string>();
            HashSet<string> visiting = new HashSet<string>();

            Visit(targetProduct, recipeMap, visited, visiting, result);

            return result;

           

            // TODO 01: output을 key로 사용하는 recipeMap을 만드세요.

            // TODO 02: 방문 완료 제품을 저장할 visited를 만드세요.

            // TODO 03: 현재 탐색 중인 제품을 저장할 visiting을 만드세요.

            // TODO 04: targetProduct부터 DFS를 시작하세요.

            // TODO 05: 탐색 중인 제품을 다시 만나면 HasCycle을 true로 만드세요.

            // TODO 06: 각 입력 재료를 먼저 재귀 탐색하세요.

            // TODO 07: 모든 입력 탐색 후 현재 제품을 Steps에 추가하세요.

            // 미완성 상태에서도 직선 제작 흐름을 볼 수 있는 임시 계획입니다.
            result.Steps.Add("IronOre");
            result.Steps.Add("IronPlate");
            result.Steps.Add(targetProduct);
            return result;
        }

        private static void Visit(string product, Dictionary<string, RecipeDefinition> recipeMap,
            HashSet<string> visited, HashSet<string> visiting, ProductionPlan result)
        {
            if (result.HasCycle || visited.Contains(product)) return;

            if (!visiting.Add(product))
            {
                result.HasCycle = true;
                return;
            }

            if (recipeMap.TryGetValue(product, out RecipeDefinition recipe))
            {
                foreach (string input in recipe.Inputs)
                    Visit(input, recipeMap, visited, visiting, result);
            }

            visiting.Remove(product);
            visited.Add(product);

            result.Steps.Add(product);
        }
    }
}
