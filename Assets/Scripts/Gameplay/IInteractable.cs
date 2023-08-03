
namespace Zong.Test.Gameplay
{
    /// <summary>
    /// This interface can be attached to any interactable in the game
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Interact method will be called when interaction needs to be performed
        /// </summary>
        /// <param name="player">Player is required to interact with the item</param>
        void Interact(Player player);
    }
}