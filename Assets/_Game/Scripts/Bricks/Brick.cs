using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : PoolMember {
    [SerializeField] private ColorData colorData;
    
    [SerializeField] private Renderer meshRenderer;
    
    public ColorType colorType;

    protected void ChangeColor(ColorType color) {
        meshRenderer.material = colorData.GetMat(color);
        this.colorType = color;
    }
}
