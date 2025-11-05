using System.Text.Json; 
using Microsoft.Maui.Controls; 
using System.IO; 

namespace Counter;

public partial class MainPage : ContentPage
{

    private int counterNumber = 0;

    private List<CounterData> counters = new List<CounterData>();

    private string dataFile = Path.Combine(FileSystem.AppDataDirectory, "CounterData.json");

    public MainPage()
    {
        InitializeComponent();

        var savedCounters = LoadCounters();

        foreach (var c in savedCounters)
        {
            AddCounter(c.Name, c.Value);
        }
    }

    private void OnAddCounterClicked(object sender, EventArgs e)
    {
        counterNumber++;
        AddCounter("Counter " + counterNumber, 0); 
        SaveCounters(); 
    }

    
    private void AddCounter(string name, int startValue)
    {
        var counterLabel = new Label
        {
            Text = startValue.ToString(),
            FontSize = 32,
            HorizontalOptions = LayoutOptions.Center
        };

        var plusButton = new Button { Text = "+" };
        var minusButton = new Button { Text = "-" };

        int value = startValue;

        plusButton.Clicked += (s, args) =>
        {
            value++;
            counterLabel.Text = value.ToString();
            UpdateCounterValue(name, value); 
            SaveCounters(); 
        };

        minusButton.Clicked += (s, args) =>
        {
            value--;
            counterLabel.Text = value.ToString();
            UpdateCounterValue(name, value);
            SaveCounters();
        };

        var buttonsLayout = new HorizontalStackLayout
        {
            Children = { minusButton, plusButton },
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 20
        };

        var counterLayout = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    Text = name, 
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center
                },
                counterLabel, 
                buttonsLayout 
            },
            Padding = 10,
            BackgroundColor = Colors.LightGray
        };

        CountersStack.Children.Add(counterLayout);


        bool exists = false;

        foreach (var c in counters)
        {
            if (c.Name == name)
            {
                exists = true;
                break; 
            }
        }

        if (!exists)
        {
            counters.Add(new CounterData { Name = name, Value = value });
        }
    }

   
    private void UpdateCounterValue(string name, int newValue)
    {
        CounterData foundCounter = null;

        foreach (var counter in counters)
        {
            if (counter.Name == name)
            {
                foundCounter = counter;
                break; 
            }
        }

        if (foundCounter != null)
        {
            foundCounter.Value = newValue;
        }
    }




    private void SaveCounters()
    {
        try
        {
            var json = JsonSerializer.Serialize(counters);
            File.WriteAllText(dataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd zapisu: " + ex.Message);
        }
    }


    private List<CounterData> LoadCounters()
    {
        try
        {
            if (File.Exists(dataFile))
            {
                var json = File.ReadAllText(dataFile);
                var data = JsonSerializer.Deserialize<List<CounterData>>(json);
                counters = data ?? new List<CounterData>();
                counterNumber = counters.Count; 
                return counters;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd odczytu: " + ex.Message);
        }

        return new List<CounterData>();
    }
}

public class CounterData
{
    public string Name { get; set; } 
    public int Value { get; set; }   
}
