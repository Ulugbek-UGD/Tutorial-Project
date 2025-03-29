using UnityEngine;

[CreateAssetMenu(fileName = "New Clicker Data", menuName = "Game Data/Clicker Data")]
public class ChickenData : ScriptableObject
{
    public int gold;
    public int egg;
    
    private int numberOfClicks;
    
    public void AddGold()
    {
        numberOfClicks++;
        gold++;
        if (numberOfClicks == 10)
        {
            AddEgg();
            numberOfClicks = 0;
        }
    }
    
    private void AddEgg()
    {
        egg++;
    }
}