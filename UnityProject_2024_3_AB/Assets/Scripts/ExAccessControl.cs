using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{
    //다른 스크립트에서 접근 가능
    public int publicValue;

    //같은 클래스 내에서만 접근 가능
    private int privateValue;

    //같은 클래스 및 파생 클래스에서 접근 가능
    protected int protectedValue;

    //같은 어셈블리(프로젝트 내 다른 스크립트)내에서 접근 가능
    internal int internalValue;

    

}
