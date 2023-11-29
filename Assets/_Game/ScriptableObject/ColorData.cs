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
        for (int i = 0; i < colors.Length; ++i) {
            if (!listColors.ContainsKey(colors[i].colorType)) {
                listColors.Add(colors[i].colorType, colors[i].mat);
            }
        }
    }

    public Material GetMat(ColorType colorType) {
        return listColors[colorType];
    }
}
