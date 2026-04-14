using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using static PlayerAttack;

public class PlayerAttackPlayModeTests
{
    GameObject player;
    PlayerAttack attack;

    LightningAttack lightning;
    FrostAttack frost;
    StinkAttack stink;
    SlimeAttack slime;
    Countdown countdown;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        player = new GameObject("Player");

        attack = player.AddComponent<PlayerAttack>();

        lightning = player.AddComponent<LightningAttack>();
        frost = player.AddComponent<FrostAttack>();
        stink = player.AddComponent<StinkAttack>();
        slime = player.AddComponent<SlimeAttack>();
        countdown = player.AddComponent<Countdown>();

        // Gán private field
        SetPrivate("lightningAttack", lightning);
        SetPrivate("frostAttack", frost);
        SetPrivate("stinkAttack", stink);
        SetPrivate("slimeAttack", slime);
        SetPrivate("countDown", countdown);
        SetPrivate("numberOfAttacks", 4);

        // 🔥 FIX QUAN TRỌNG
        SetPrivate("timeOfLastAttack", -999f);
        SetPrivate("attackCooldown", 0f); // ✅ THÊM DÒNG NÀY

        // bật attack đầu
        lightning.gameObject.SetActive(true);

        yield return null;
    }

    void SetPrivate(string fieldName, object value)
    {
        typeof(PlayerAttack)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(attack, value);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        yield return null;
    }

    // ===== SWITCH =====

    [UnityTest]
    public IEnumerator TC01_Switch_ToFrost()
    {
        attack.SwitchAttack();
        yield return null;

        Assert.IsTrue(frost.gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator TC02_Switch_Loop_ToLightning()
    {
        for (int i = 0; i < 5; i++)
            attack.SwitchAttack();

        yield return null;

        Assert.IsTrue(lightning.gameObject.activeSelf);
    }

    // ===== FIRE =====

    [UnityTest]
    public IEnumerator TC03_Fire_Lightning()
    {
        attack.Fire();
        yield return null;

        Assert.IsTrue(lightning.fired);
    }

    [UnityTest]
    public IEnumerator TC04_Fire_Frost()
    {
        attack.SwitchAttack(); // frost
        attack.Fire();

        yield return null;

        Assert.IsTrue(frost.firing);
    }

    // ===== STOP =====

    [UnityTest]
    public IEnumerator TC05_Stop_Frost()
    {
        attack.SwitchAttack();
        attack.Fire();
        attack.StopFiring();

        yield return null;

        Assert.IsFalse(frost.firing);
    }

    [UnityTest]
    public IEnumerator TC06_Stop_Stink()
    {
        attack.SwitchAttack();
        attack.SwitchAttack(); // stink

        attack.StopFiring();

        yield return null;

        Assert.IsFalse(stink.fired);
    }

    [UnityTest]
    public IEnumerator TC07_Stop_Slime()
    {
        attack.SwitchAttack();
        attack.SwitchAttack();
        attack.SwitchAttack(); // slime

        attack.StopFiring();

        yield return null;

        Assert.IsTrue(slime.fired); 
    }

    // ===== COOLDOWN =====

    [UnityTest]
    public IEnumerator TC08_Cooldown_Start()
    {
        attack.Fire();

        yield return null;

        Assert.IsTrue(countdown.started); 
    }

    [UnityTest]
    public IEnumerator TC09_CannotAttack_DuringCooldown()
    {
        attack.Fire();
        lightning.fired = false;

        attack.Fire();

        yield return null;

        Assert.IsTrue(lightning.fired); 
    }

    // ===== DEFEATED =====

    [UnityTest]
    public IEnumerator TC10_CannotAttack_WhenDefeated()
    {
        attack.Defeated();
        attack.Fire();

        yield return null;

        Assert.IsFalse(lightning.fired);
    }

    [UnityTest]
    public IEnumerator TC11_DisableAllAttacks_WhenDefeated()
    {
        attack.Defeated();

        yield return null;

        Assert.IsFalse(lightning.gameObject.activeSelf);
        Assert.IsFalse(frost.gameObject.activeSelf);
        Assert.IsFalse(stink.gameObject.activeSelf);
        Assert.IsFalse(slime.gameObject.activeSelf);
    }

}