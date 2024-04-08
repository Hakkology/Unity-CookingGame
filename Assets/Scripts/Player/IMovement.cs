public interface IMovement
{
    void Init();    // Initialize the state
    void Update();  // Update the state in each frame
    void Cancel();  // Cancel the state if necessary
}