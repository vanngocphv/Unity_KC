using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IF_HasProgress
{
    public event EventHandler<OnProgressBarChangeUIArgs> OnProgressBarChangeUI;
    public class OnProgressBarChangeUIArgs {
        public float changeAmount;
    }
}
