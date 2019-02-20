# GridPanel

A WPF Panel for ItemsControl that's works like a grid.

How to use it ?

First, add the namespace :

```
xmlns:samsoft="clr-namespace:SamSoft.Windows"
```

Then you can integrate it as ItemsPanelTemplate like this : 

```
<ItemsControl Margin="5" Grid.Column="1" ItemsSource="{Binding Items}" ItemTemplate="{StaticResource ButtonTemplate}">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <GridPanels:GridPanel Orientation="Vertical" />
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
</ItemsControl>
```

It's possible to change the orientation of the GridPanel with values Vertical or Horizontal. 

![Image Grid](GridPanel/Images/GridPanel.jpg)

[Download GridPanel now!](GridPanel/GridPanel/GridPanel.cs) 


