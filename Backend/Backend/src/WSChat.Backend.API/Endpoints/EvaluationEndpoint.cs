namespace Polichat_Backend.Endpoints;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Evaluation
{
    
}

public class EvaluationEndpoint
{
    public class Fields
    {
        public Fields(double startX, double startY, double endX, double endY, string ideologyName, string ideologyDescription)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            IdeologyName = ideologyName;
            IdeologyDescription = ideologyDescription;
        }

        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public string IdeologyName { get; set; }
        public string IdeologyDescription { get; set; }
    }
    

    public static IDictionary<string,string> evaluation(double x, double y)
    {
        
        bool inRange(double value, double min, double max) => ((value - max)*(value - min) <= 0.0);
        IDictionary<string, string> returnMe = new Dictionary<string, string>();
        
        IDictionary<int, Fields> IdeologyDictionary = new Dictionary<int, Fields>();    
        IdeologyDictionary.Add(5,new Fields(0.43,0.72,0.71,1.0,"Falangism","Falangism, is an economically third position (but usually left-leaning), culturally far right-wing, Ethnonat.png ethnonationalist (but highly supports race mixing, claiming that the intermixing of the Spanish race and other races has produced a Hispanic supercaste that is ethically improved, morally robust, spiritually vigorous) authoritarian and nationalist ideology originating from Spain. It places a large emphasis on hierarchy, authority and order, and is opposed to communism and liberal democracy. Falangism believe in uniting all of Spain using violence, thus it heavily opposes regional Separatist movements. It believes in a form of PanHisp.png pan-Hispanic nationalism known as Hispinidad. It also believes in strict adherence to traditional gender roles."));
        IdeologyDictionary.Add(6,new Fields(0.72,0.72,1.0,1.0,"Absolute Monarchy","Absolute Monarchism (AbMon), or Absolutism, is an authoritarian ideology which advocates for a single monarch with absolute rule over the state who is not bound by any formal rules, often being seen as owning the countries they reside over. Such rule is often justified through Religious.png religious means though some modern proponents of this ideology use Nrx.png secular pragmatists arguments"));

        IdeologyDictionary.Add(12,new Fields(0.43,0.43,0.71,0.71,"Feudalism","Feudalism is an authoritarian, decentralized, economically and culturally right-wing ideology practiced throughout Europe from as early as the 5th century to as late as technically 2008 (Channel Island of Sark) although its prime was from the 9th century- when the middle ages properly kicked off- to the 17th century- when Monkeyzz-Enlightenment.png Enlightenment had properly started to create an entirely new status quo."));
        IdeologyDictionary.Add(13,new Fields(0.72,0.43,1.0,0.71,"National Capitalism","National Capitalism, often referred to by the abbreviations NazCap or NatCap[1] is a totalitarian, economically and culturally far-right, nationalist and fascist[2] ideology that combines Nazi.png National Socialist cultural, civil and diplomatic principles with free market or laissez-faire capitalism, making it an even more extreme version of Pinochet.png Pinochetism (whose Sec.pngauthoritarianism is based primarily on dealing with political opposition). He inhabits the top right corner of the political compass, with Pinochet.png Pinochetism coming closest to him"));
       
        IdeologyDictionary.Add(19,new Fields(0.43,0.15,0.71,0.43,"Elective Monarchy","Elective Monarchism, shortened to Elecmon, is a non-quadrant monarchist ideology. Elecmon was born as an alternative to lineage based monarchies who had a tendency to result in succession crisis after a few generations. The voting powers are usually only held by high ranking nobles and influential clergy members. "));
        IdeologyDictionary.Add(20, new Fields(0.72,0.14,1.0,0.42,"Corporatism","Corporatism is a economically third position, authoritarian ideology that advocates for the organization of society in different areas of employment like Farm.png agriculture, Strato.png military, Synd.png engineering etc called corporations which the government assigns people to and your employment into these is designated by your interests and skills, then the government sets a goal like for example the production of 40,000 cars per month with the expectation the standard is met. He believes in a cross-class system of regulation, in which the workers, employers, and state negotiate with one another in order to most efficiently run the economy and satisfy (in theory) all parties involved. "));
        IdeologyDictionary.Add(27,new Fields(0.72,-0.13,1.0,0.13,"Capitalism","Capitalism is an ideology and economic system representing a broad range of ideologies that fall under the umbrella of capitalism. Capitalism is defined in many different ways, by Soc.png socialists it's usually defined as a system where the bourgeoisie exploit the proletariat, by Anmark2.png market anarchists it's usually defined as Corp.png corrupt markets and by Libertarian.png Right-Libertarianism it's usually defined as a free enterprise system."));
        IdeologyDictionary.Add(34, new Fields(0.72,-0.42,1.0,-0.14,"Libertarianism","Libertarianism, or more simply Right-Libertarianism or Libertarian Capitalism, is a civically libertarian, Lfree.png laissez-faire capitalist and culturally variable ideology. He inhabits the Libright-yellow.png libertarian right quadrant of the political compass, generally being in the middle of it unless specified.He believes in a very limited government and the individual's natural self evident rights of life, liberty, and property. He likes the use of militias to watch them.He technically believes in the same principles of Clib.png classical liberalism of equality before the law and the basic rights to life, liberty, and property, along with most librights, although some people debate most libertarians are only libertarians because of the precise ideology and not the principles of it."));
        IdeologyDictionary.Add(41,new Fields(0.72,-0.71,1.0,-0.43,"Paleolibertarianism","Paleolibertarianism (shortened to Paleobert and Paleolib) also called Old Right Libertarianism is a type of Libertarian.png Libertarian sub-ideology which stresses inherent incompatibility between cultural egalitarianism/Progress.png progressivism, and inclusionarism, and the concept of liberty, as well as a focus on the importance of inherited culture as a means of maintaining order.Paleolibertarianism is distinguished by a particular opposition to all types of state interventionism, especially Necon.png Foreign and Regulationism.png Economic. Paleolibertarianism as an ideology is one which embodies a culturally and economically right-wing and a civically libertarian position."));
        IdeologyDictionary.Add(48,new Fields(0.72,-1.0,1.0,-0.72,"Anarcho-Capitalism","Anarcho-Capitalism (AnCap), also called Private Property Anarchy, Private Law Society,[5] and Rothbardianism,[6] as well a bunch of other names,[7] is a political ideology, as well as a theoretical social order, based around Clib.png Classical Liberal conception of Property.png property rights, Indiv.png individualism, and Awaj.png rejection of the state but lead to its logical conclusion, the elimination of it."));
        
        /*
        IdeologyDictionary.Add(1, new Fields(-0.72, 0.72, -1.0, 1.0, "Stalinism", "Stalinism is the means of governing and Marxist-Leninist policies implemented in the Soviet Union from 1927 to 1953 by Joseph Stalin.", "https://en.wikipedia.org/wiki/Stalinism"));
        IdeologyDictionary.Add(7, new Fields(-0.72, 0.43, -1,0.71 , "Marxism-Leninism", "Marxism–Leninism is an authoritarian, economically far left ideology that can range from culturally left to culturally right (But usually culturally left). It is based on the Joseph Stalin's ideological synthesis of Orthodox Marxism and Leninism, although many self-proclaimed Marxist-Leninist leaders were opposed to Joseph Stalin, such as Fidel Castro and Nikita Khrushchev.", "https://en.wikipedia.org/wiki/Marxism%E2%80%93Leninism"));
        IdeologyDictionary.Add(14, new Fields(-0.72, 0.14, -1, 0.42, "Trotskyism", "Trotskyism is the political ideology and branch of Marxism developed by Ukrainian-Russian revolutionary Leon Trotsky and by some other members of the Left Opposition and Fourth International.", "https://en.wikipedia.org/wiki/Trotskyism"));
        IdeologyDictionary.Add(21, new Fields(-0.72, -0.13, -1, 0.13, "Posadism", "The Fourth International Posadist is a Trotskyist international. It was founded in 1962 by J. Posadas, who had been the leader of the Latin America Bureau of the Fourth International in the 1950s, and of the Fourth International's section in Argentina.","https://en.wikipedia.org/wiki/Fourth_International_Posadist"));
        IdeologyDictionary.Add(28, new Fields(-0.72, -0.14, -1, -0.42, "Luxenburgism", "Rosa Luxenburg was a Polish and naturalised-German revolutionary socialist, Marxist philosopher and anti-war activist. Successively, she was a member of the Proletariat party, the Social Democracy of the Kingdom of Poland and Lithuania, the Communist Party of Germany (KPD), and many more. ", "https://en.wikipedia.org/wiki/Rosa_Luxemburg#Thought"));
        IdeologyDictionary.Add(35, new Fields(-0.72, -0.43, -1, -0.71, "Council Communism", "Council communism is a current of communist thought that emerged in the 1920s. Inspired by the November Revolution, council communism was opposed to state socialism and advocated workers' councils and council democracy. Strong in Germany and the Netherlands during the 1920s, council communism continues to exist as a small minority in the left.", "https://en.wikipedia.org/wiki/Council_communism"));
        IdeologyDictionary.Add(42, new Fields(-0.72, -0.72, -1.0, -1.0, "Anarcho-communism", "Anarcho-communism,also known as anarchist communism,is a political philosophy and anarchist school of thought which advocates the abolition of the state, capitalism, wage labour, social hierarchies and private property.", "https://en.wikipedia.org/wiki/Anarcho-communism"));
        */
        
        foreach (var VARIABLE in IdeologyDictionary)
        {
            if(inRange(x,VARIABLE.Value.StartX,VARIABLE.Value.EndX) && 
               inRange(y,VARIABLE.Value.StartY,VARIABLE.Value.EndY))
            {
                returnMe.Add(VARIABLE.Value.IdeologyName, VARIABLE.Value.IdeologyDescription);
                Console.WriteLine();
                Console.WriteLine(VARIABLE.Value.IdeologyName);
                break;
            }
        }
        return returnMe;
    }

    // public static void Main(string[] args)
    // {
    //     evaluation(0.80, -0.6);
    // }
}

