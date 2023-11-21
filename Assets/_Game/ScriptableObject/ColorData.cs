using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject {
    [SerializeField] private Colors[] colors;
    
    [System.Serializable]
    private struct Colors {
        public ColorType colorType;
        public Material mat;
    }

    private Dictionary<ColorType, Material> listColors = new Dictionary<ColorType, Material>();

    private void OnEnable() {
        foreach (var item in colors) {
            if (!listColors.ContainsKey(item.colorType)) {
                listColors.Add(item.colorType, item.mat);
            }
        }
    }

    public Material GetMat(ColorType colorType) {
        return listColors[colorType];
    }
}
