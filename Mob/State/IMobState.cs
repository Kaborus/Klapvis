using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMobState
{
    void Handle(MobController mobController);
}
