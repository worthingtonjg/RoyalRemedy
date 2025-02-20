using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Potion 
{
    public Color color { get; set;}
    public string name { get; set;}
    public string symptom { get; set; }
}

public class Potions : MonoBehaviour
{
    private List<Color> _colors = new List<Color> () {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,  
        Color.magenta,
        Color.cyan,
    };

    private List<string> _potions = new List<string>() {
        "Moonflower Petals",
        "Crystalized Manticore Venom",
        "Phoenix Ash",
        "Tears of a Willow Sprite",
        "Powdered Dragonbone",
        "Nightshade Berries",
        "Essence of Starlight",
        "Frost Lotus",
        "Liquid Sunlight",
        "Specter’s Breath",
        "Goldenroot Extract",
        "Ethereal Moth Dust",
        "Obsidian Shard",
        "Glowcap Mushrooms",
        "Blood of the Basilisk",
        "Amberleaf Sap",
        "Whispering Fern Leaves",
        "Celestial Pearl",
        "Venomvine Thorn",
        "Silverdew Drops"        
    };

    private List<string> _symptoms = new List<string> () { 
        "Blurred Vision",
        "Nausea and Vomiting",
        "Weakness or Fatigue",
        "Burning Sensation in the Throat",
        "Hallucinations",
        "Severe Muscle Cramps",
        "Paralysis of Limbs",
        "Dizziness or Vertigo",
        "Shortness of Breath",
        "Darkening or Discoloration of Skin",
        "Sudden Uncontrollable Shivering",
        "Heart Palpitations",
        "Fever or Chills",
        "Unexplained Bleeding",
        "Sudden Loss of Voice",
        "Excessive Sweating",
        "Unrelenting Itching or Rash",
        "Severe Headache or Migraine",
        "Unusual Lethargy or Sleepiness",
        "Sharp Stomach Pain",
        "Glowing Ears",
        "Hiccups That Echo Loudly",
        "Sudden Inflatable Limbs",
        "Temporary Loss of Gravity",
        "Involuntary Dance Moves",
        "Hair Turning Into Grass or Feathers",
        "Speaking Backward Only",
        "Unstoppable Laughter",
        "Instant Mustache Growth ",
        "Uncontrollable Rhyming When Talking",
        "Sudden Aversion to Shoes",
        "Eyes That Change Colors Every Few Seconds",
        "Temporary Invisible Head Syndrome",
        "Extreme Attraction to Inanimate Objects",
        "Voice Pitch Shifting Like a Karaoke Machine",
        "Feet That Refuse to Move in the Same Direction",
        "Chronic Banana Cravings",
        "Ears That Wont Stop Twitching"
    };

    public List<Potion> Generate(List<Color> solution)
    {
        var result = new List<Potion>();

        var localPotions = _potions.ToList();
        var localSymptoms = _symptoms.ToList();

        var random = new System.Random();

        // assign random names to each color
        var potionNames = new Dictionary<Color, string>();
        foreach(var color in _colors)
        {
            int potionIndex = random.Next(0, localPotions.Count);
            potionNames[color] = localPotions[potionIndex];
            localPotions.RemoveAt(potionIndex);
        }

        // Make a potion for each item in the solution
        var potionsToMake = solution.ToList();

        // Add potions for the colors that aren't in the solution
        _colors.ForEach(c => {
            if (!solution.Contains(c))
            {
                potionsToMake.Add(c);
            }
        });

        // assign random potions and symptoms to each color

        for (int i = 0; i < solution.Count; i++)
        {
            var potion = new Potion();
            potion.color = _colors[i];
            potion.name = potionNames[potion.color];

            int symptomIndex = random.Next(0, localSymptoms.Count);
            potion.symptom = localSymptoms[symptomIndex];
            localSymptoms.RemoveAt(symptomIndex);

            result.Add(potion);

            //print(potion.color + " - " + potion.name + " - " + potion.symptom);
        }

        return result;
    }

    void Start()
    {
        Generate(new List<Color> {Color.red, Color.blue, Color.yellow, Color.magenta});
    }
}
