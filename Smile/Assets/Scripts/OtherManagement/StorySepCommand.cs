using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySepCommand : Singleton<StorySepCommand>
{
    public enum commandNum
    {
        First, CutEnter, CutEnter_E, CutEnd, KilledBy, BossFail, BossSuccess
    }

    private commandNum commandBranch = commandNum.First;

    public void setCommandBranch(commandNum commandBranch)
    {
        this.commandBranch = commandBranch;
    }

    public commandNum getCommandBranch()
    {
        return this.commandBranch;
    }
}
