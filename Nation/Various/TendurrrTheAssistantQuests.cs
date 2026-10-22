/*
name: TendurrrTheAssistantQuests
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class TendurrrTheAssistantQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    private static CoreFarms Farm
    {
        get => _Farm ??= new CoreFarms();
        set => _Farm = value;
    }
    private static CoreFarms _Farm;
    private static CoreNation Nation
    {
        get => _Nation ??= new CoreNation();
        set => _Nation = value;
    }
    private static CoreNation _Nation;

    public string OptionsStorage = "Reward Select";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<RewardsSelection>(
            "RewardsSelection",
            "Select Your Quest Reward",
            "Select Your Quest Reward for The Tendurr items of Nulgath quest.",
            RewardsSelection.Tendurrr_on_your_Back
        ),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        TendurrItems(Bot.Config!.Get<RewardsSelection>("RewardsSelection"));

        Core.SetOptions(false);
    }

    /// <summary>
    /// Farms the selected Tendurrr The Assistant quest reward or all available rewards.
    /// </summary>
    /// <param name="reward">The reward to obtain, or All to obtain every reward.</param>
    public void TendurrItems(RewardsSelection reward = RewardsSelection.All)
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);

        if (!Core.CheckInventory("Tendurrr The Assistant"))
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Tendurrr The Assistant", isTemp: false);

        Nation.FarmUni13(1);

        List<ItemBase> rewards = Core.EnsureLoad(5814).Rewards;
        ItemBase? item = reward == RewardsSelection.All
            ? null
            : rewards.Find(x => x.Name == Rewards[(int)reward]);

        if (reward != RewardsSelection.All && item == null)
        {
            Core.Logger($"Could not find the quest reward for {reward}.");
            return;
        }

        int count = Rewards.Length;
        int x = 1;

        while (
            !Bot.ShouldExit
            && (
                reward == RewardsSelection.All
                    ? !Core.CheckInventory(Rewards, toInv: false)
                    : !Core.CheckInventory(item!.ID, toInv: false)
            )
        )
        {
            if (reward == RewardsSelection.All)
                Core.Logger($"Farming All {x++}/{count}");
            else
                Core.Logger($"Farming {item!.Name}");

            Core.EnsureAccept(5814);
            Nation.ApprovalAndFavor(500, 500);
            Nation.EssenceofNulgath(20);
            Nation.FarmGemofNulgath(30);
            Nation.FarmDarkCrystalShard(30);
            Nation.FarmTaintedGem(30);
            Nation.FarmDiamondofNulgath(30);
            Nation.FarmVoucher(member: true);
            Nation.FarmBloodGem(10);
            Nation.FarmTotemofNulgath(3);

            if (reward == RewardsSelection.All)
                Core.EnsureCompleteChoose(5814);
            else
                Core.EnsureComplete(5814, item!.ID);
        }
    }



    public readonly string[] Rewards =
    {
        "Tendurrr on your Back",
        "Asuka's Flaming Stick",
        "Tendurrr's Flaming Stick",
        "Tendurrr Morph",
    };

    public enum RewardsSelection
    {
        Tendurrr_on_your_Back = 0,
        Asuka_Flaming_Stick = 1,
        Tendurrr_Flaming_Stick = 2,
        Tendurrr_Morph = 3,
        All,
    };
}
