namespace Zong.Test.Gameplay
{
    /// <summary>
    /// This interface can be attached to any objective
    /// </summary>
    public interface IObjective
    {
        /// <summary>
        /// Execute will be called when objective is required to be executed
        /// </summary>
        /// <param name="item">Item is required to perform the objective</param>
        void Execute(Item item, Player player);
    }
}