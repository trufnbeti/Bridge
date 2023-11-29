using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : PoolMember {
    public ColorType colorType;
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer render;

    public void ChangeColor(ColorType color) {
        if (color == ColorType.None) {
            render.enabled = false;
        } else {
            render.material = colorData.GetMat(color);
            render.enabled = true;
        }
        colorType = color;

    }

    public override void OnInit() {
        
    }
    
    public override void OnDespawn() {
        SimplePool.Despawn(this);
    }
}
