# BattleUnitsTest

**Описание**:<br />
Obstacle - препятствие (размер, позиция)<br />
ObstacleList - список препятствий<br />
Grid - класс для поиска пути, знает от препятствиях<br />

UnitData - scriptable object с настройками юнитов<br />
Unit - скрипт юнита (движение, атака)<br />
ArmyController - список юнитов, метод поиска ближайшего юнита к требуемой позиции<br />
Game - содержит Grid и 2 ArmyController (Зомби и Люди)<br />
GameUI - UI для игры<br />

События Unit при смерти извещает об этом ArmyController (удаляет из списка), далее извещается Game, далее реагирует GameUI(обновляет UI)
