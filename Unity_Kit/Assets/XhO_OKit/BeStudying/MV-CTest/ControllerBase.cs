using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ControllerBase<T> where T :IModelView
{
    protected T modelView;
}
