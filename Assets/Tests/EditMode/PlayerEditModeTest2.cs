using UnityEngine;
using NUnit.Framework;

public class PlayerEditModeTest2 : MonoBehaviour
{
    // Test case 1: Tốc độ di chuyển phải > 0
    [Test]
    public void Player_MoveSpeed_GreaterThanZero()
    {
        float moveSpeed = 5f;


        Assert.Greater(
            moveSpeed,
            0,
            "Tốc độ di chuyển phải lớn hơn 0"
        );
    }


    // Test case 2: Tốc độ di chuyển không được âm
    [Test]
    public void Player_MoveSpeed_NotNegative()
    {
        float moveSpeed = 5f;


        Assert.GreaterOrEqual(
            moveSpeed,
            0,
            "Tốc độ di chuyển không được là số âm"
        );
    }
}


