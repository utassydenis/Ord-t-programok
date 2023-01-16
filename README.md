# Fordítóprogramok táblázatos elemző.

## 1. Működése


* A program beolvas egy CSV formátumú táblázatot, ami tartalmazza a nyelvtan szabályait.
* A program nem csak egyféle szabályrendszert tud használni.

A program által használt nyelvtan:
G = ({E, E’, T, T’, F}, {+, *, (, ), i}, P, E)

S -> E#
E -> TE’
E’ -> +TE’ | e
T -> FT’
T’ -> *FT’ | e
F -> (E) | i


* A program, ha kell átalakítja a bemeneti adatokat a megfelelő formátumra:
* Például:( 3 * 4 ) + 2 kifejezésből az ( i + i ) * i egyszerűsített formulát hoz létre.
* Meg lehet adni egyből az egyszerűsített formulát is.

Egy tuple használatával írja ki a az input szalag aktuális elemeit, a verem aktuális elemeit és az eddig alkalmazott szabályokat.

## 2. Algoritmus:
1. Bekérem az inputot és ,ha kell, átalakítom a megfelelő alakra.
2. A verembe belerakom a táblázat alapján a start szimbólumot.
3. A táblázat és az input alapján kiválasztom a szabályokat a datagridview sor és oszlop koordinátái alapján.
4. A kiolvasott szabály elvégzése után folytatom a szabályok alkalmazását amíg meg nem áll a szabályok alapján.
5. A program ezután elfogadja vagy elutasítja az inputot.