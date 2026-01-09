# 🏭 Warehouse Management Simulator (Unity)

![Unity](https://img.shields.io/badge/Unity-3D-black?logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-blue)
![Status](https://img.shields.io/badge/Status-Functional-success)
![License](https://img.shields.io/badge/Project-Academic-informational)

> **Simulation interactive de gestion d’entrepôt en environnement 3D (FPS)**
> Orientée **logique métier**, **gestion de données** et **interaction temps réel**.

---

## 📖 Table des matières

1. [Description générale](#-description-générale)
2. [Objectifs du projet](#-objectifs-du-projet)
3. [Technologies utilisées](#️-technologies-utilisées)
4. [Architecture globale](#-architecture-globale)
5. [Données – Système JSON](#-données--système-json)
6. [Gestion des produits](#️-gestion-des-produits-warehouseitem)
7. [Déplacement & caméra](#-déplacement--caméra-fpscontroller)
8. [Sélection des objets](#️-sélection-des-objets-warehouseitemselector)
9. [Actions métier](#-actions-métier-actionmanager)
10. [État global de l’entrepôt](#-état-global-de-lentrepôt-gamemanager)
11. [Interface utilisateur](#️-interface-utilisateur-ui)
12. [Organisation de la scène](#-organisation-de-la-scène)
13. [État actuel du projet](#-état-actuel-du-projet)
14. [Évolutions prévues](#-évolutions-prévues)
15. [Conclusion](#-conclusion)

---

## 📌 Description générale

**Warehouse Management Simulator** est une application développée avec **Unity** simulant la gestion d’un **entrepôt moderne**.

Le joueur évolue en **vue FPS** dans un environnement 3D et interagit directement avec les produits afin de :

* surveiller les niveaux de stock,
* détecter les anomalies,
* corriger les erreurs,
* stabiliser l’ensemble de l’entrepôt.

🎯 Le projet met volontairement l’accent sur la **logique métier**, la **gestion de données** et la **structuration logicielle**, plus que sur l’aspect purement ludique.

---

## 🎯 Objectifs du projet

* Simuler un système réaliste de gestion de stock
* Manipuler des données dynamiques (**JSON → Unity**)
* Implémenter une logique métier claire et lisible
* Offrir une interaction intuitive en environnement 3D
* Construire une base **extensible** vers :

  * commandes clients
  * catégorisation avancée
  * analytics & IA

---

## 🛠️ Technologies utilisées

* **Unity 3D**
* **C#**
* **TextMeshPro** (UI)
* **JSON** (données)
* **PhysX & Raycasting**
* **FPS Controller personnalisé**

---

## 🧩 Architecture globale

Le projet suit une architecture modulaire avec une séparation claire des responsabilités :

```text
Data (JSON)
   ↓
DataManager
   ↓
WarehouseItem (Scene)
   ↓
Selection & Actions
   ↓
UI & Feedback
   ↓
GameManager (État global)
```

✔️ Cette structure facilite la maintenance et l’évolution du projet.

---

## 📂 Données – Système JSON

Les produits sont définis dans un fichier JSON externe.

```json
{
  "items": [
    { "id": "Item_Food_01", "name": "Riz", "quantity": 5, "warningThreshold": 10 },
    { "id": "Item_Electronics_01", "name": "Iphone 17", "quantity": 0, "warningThreshold": 10 },
    { "id": "Item_Clothes_01", "name": "Tshirt Prada", "quantity": 25, "warningThreshold": 10 }
  ]
}
```

### Fonctionnalités clés

* Chargement automatique au démarrage
* Association par `id` → **nom exact du GameObject**
* Application dynamique des données à la scène
* Séparation claire **Data / Visuel**

---

## 🏷️ Gestion des produits (`WarehouseItem`)

Chaque produit est représenté par un objet Unity possédant un script `WarehouseItem`.

### Attributs

* Nom du produit
* Quantité
* Seuil d’alerte
* Statut du stock

### Statuts possibles

| Statut     | Condition        |
| ---------- | ---------------- |
| 🟢 OK      | Quantité > seuil |
| 🟡 Warning | Quantité ≤ seuil |
| 🔴 Error   | Quantité ≤ 0     |

👉 Le changement de statut entraîne automatiquement un **feedback visuel** (couleur).

---

## 🎮 Déplacement & caméra (`FPSController`)

* Déplacement libre dans l’entrepôt (WASD)
* Rotation caméra à la souris
* Curseur verrouillé
* Sensibilité configurable
* Gestion des collisions via `CharacterController`

---

## 🖱️ Sélection des objets (`WarehouseItemSelector`)

La sélection se fait par **clic souris** à l’aide du raycasting.

### Lorsqu’un objet est sélectionné :

* 🔼 Il se **surélève légèrement**
* ⬛ Un **outline noir** s’active
* 🧾 Les informations s’affichent dans l’UI
* 🔄 L’objet précédemment sélectionné est réinitialisé

---

## 🧠 Actions métier (`ActionManager`)

Actions disponibles sur l’objet sélectionné :

| Touche | Action                            |
| ------ | --------------------------------- |
| **R**  | Restock (+ quantité configurable) |
| **F**  | Fix (corrige un item en Error)    |

✔️ Feedback UI temporaire
✔️ Fallback en `Debug.Log` si UI absente

---

## 📊 État global de l’entrepôt (`GameManager`)

Analyse en temps réel de l’ensemble des produits :

* Nombre d’items OK / Warning / Error
* Indicateur de progression globale

### Validation finale

* ✅ **ENTREPÔT STABILISÉ** → tous les items sont OK
* ❌ **ACTION REQUISE** → sinon

---

## 🖥️ Interface Utilisateur (UI)

* Canvas en **Screen Space**
* Textes dynamiques via **TextMeshPro**
* UI lisible et non intrusive
* Séparation stricte UI / logique métier

---

## 🧱 Organisation de la scène

Chaque item :

* possède un `Collider`
* possède un `WarehouseItem`
* contient un enfant `Outline`

---

## 🚀 État actuel du projet

### ✅ Fonctionnel

* Gestion complète des stocks
* Interaction FPS fluide
* Chargement dynamique des données
* Feedback visuel & UI
* Logique métier stable

---

## 🔮 Évolutions prévues

* Commandes multi-produits
* Décrément automatique des stocks
* Catégories formelles
* Dashboards & analytics
* IA de prédiction de rupture
* Sauvegarde persistante
* Mode administrateur

---

## ⭐ Conclusion

Ce projet constitue une **base solide de système de gestion logistique interactive**, proprement architecturée et prête à évoluer vers des usages avancés (simulation, data, intelligence artificielle).

---

