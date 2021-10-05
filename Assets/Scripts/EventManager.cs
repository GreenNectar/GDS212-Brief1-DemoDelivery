using DemoDelivery.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Created by Michael and Jarrad

namespace DemoDelivery
{
    public static class EventManager
    {
        public static UnityEvent<int> onButtonPress = new UnityEvent<int>();
        public static UnityEvent<int> onButtonRelease = new UnityEvent<int>();

        public static UnityEvent onTogglePlay = new UnityEvent();

        public static UnityEvent onDestroyAllExplosives = new UnityEvent();
        public static UnityEvent<Explosive> onChangeExplosive = new UnityEvent<Explosive>();
    }
}