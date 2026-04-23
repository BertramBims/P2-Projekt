using UnityEngine;

public class PushedAnimator : MonoBehaviour
{
    public void ChangePushedNumberPosition(int number)
    {
        Debug.Log(number);
        GameObject.Find("Player_Mobility").GetComponent<PlayerMobilityController>().beingPushedMovementSpot = number;
    }
}
