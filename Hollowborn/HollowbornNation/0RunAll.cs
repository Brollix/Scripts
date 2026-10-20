/*
name: Hollowborn Archfiend - Run All
description: does all of Hollowborn Archfiend
tags: hollowborn, hollowborn archfiend, hollowbornarchfiend, hollowborn nation, do all, run all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/Materials/HollowSoul.cs
//cs_include Scripts/Hollowborn/HollowbornOblivionBlade.cs
//cs_include Scripts/Nation/Various/ShadowLegacyofNulgath.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Nation/Various/EnchantedNulgathNationHouse.cs
//cs_include Scripts/Hollowborn/HollowbornNation/CoreHollowbornArchfiend.cs
using Skua.Core.Interfaces;

public class HBA0
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private static CoreHollowbornArchfiend sHBA
    {
        get => _sHBA ??= new CoreHollowbornArchfiend();
        set => _sHBA = value;
    }
    private static CoreHollowbornArchfiend _sHBA;

    public string OptionsStorage = sHBA.OptionsStorage;
    public bool DontPreconfigure = true;
    public List<IOption> Options = sHBA.Options;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        sHBA.GetAll();

        Core.SetOptions(false);
    }
}
