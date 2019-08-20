# WPFMatlabPlotter
## What?
![WoW](https://github.com/teapottiger/WPFMatlabPlotter/blob/master/Images/img_0.png)

A lightweight library for embedding Matlab output window (graphs, plots) inside C# WPF x64 applications.

| Features | Supported |
|----------|------------ |
| Windows | ✔ |
| Single C# dll | ✔ |
| Open Source | ✔ |
| MIT license | ✔ |

# How?
### 1. Make sure that you have installed a MATLAB Runtime
Latest release can be found [here](https://www.mathworks.com/products/compiler/matlab-runtime.html).
### 2. Add a reference to WPFMatlabPlotter
Use the NuGet package manager to add a reference to [WPFMatlabPlotter](https://www.nuget.org/packages/WPFMatlabPlotter/).
### 3. Define MatlabPlot in XAML
```xaml
          <WPFMatlabPlotter:MatlabPlot x:Name="MainPlot" Width="300" Height="300" Margin="10" BorderBrush="Black" BorderThickness="1"/>
```
### 4. Build a plot
```csharp
                //1. Call YOUR OWN Matlab .dll to draw a plot.
                _matlabController.matlab_plot();
                //2. Call WPFMatlabPlotter
                MainPlot.BuildGraph("Figure 1");
```
### 5. Destroy a plot (if you want to close it)
```csharp
                MainPlot.DestroyGraph();
```

# Examples
You can find examples in the `/Examples` folder in the code repository.
# License
MIT - do whatever you want.
