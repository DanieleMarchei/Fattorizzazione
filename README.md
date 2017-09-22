# Fattorizzazione
Una piccola libreria di algoritmi di fattorizzazione e di strumenti matematici. Scritta per la mia tesi di laurea.

### Algoritmi di fattorizzazione
- Crivello di Eratostene 
```cs 
new CrivelloDiEratostene();
```
- Metodo di Fermat
```cs 
new MetodoDiFermat();
```
- Algoritmo di Shor
```cs 
new Shor();
```
- Fattorizzazione per curve ellittiche
```cs 
new MetodoACurveEllittiche();
```
- Crivello quadratico **TODO**
- Crivello dei campi di numeri generale **TODO**

### Come usare la libreria

```cs
IAlgoritmo algo = new MetodoACurveEllittiche();
List<ulong> fattori = algo.Fattorizza(42);
Utility.StampaASchermo(fattori);
```

### Idee
- Probabile aggiunta di benchmark per confrontare i vari algoritmi

### Librerie utilizzate
- BigInteger [https://github.com/bazzilic/BigInteger](https://github.com/bazzilic/BigInteger)


