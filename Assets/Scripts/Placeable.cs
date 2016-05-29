using UnityEngine;
using System.Collections;

public interface Placeable {

    int getCost();
    Sprite getIcon();
    void setPlaced(bool place);


}
