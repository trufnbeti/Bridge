using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : PoolMember {
    public ColorType colorType;
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer render;

    public void ChangeColor(ColorType color) {
        render.material = colorData.GetMat(color);
        colorType = color;
        if (!render.enabled) {
            render.enabled = true;
        }

        if (color == ColorType.None) {
            render.enabled = false;
        }
    }

    public override void OnInit() {
        
    }
    
    public override void OnDespawn() {
        SimplePool.Despawn(this);
    }
}
