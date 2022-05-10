using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Services;

namespace Polichat_Backend.Controllers;

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

public record IdeologyInfo(string Name, string Description);

[ApiController]
public class EvaluationEndpoint
{
    private AnalyticsService _analyticsService;
    public EvaluationEndpoint(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    
    [Route("/evaluation")]
    [HttpGet]
    public async Task<IdeologyInfo> Evaluation(double x, double y)
    {
        _analyticsService.EvaluationAnalytics++;
        bool InRange(double value, double min, double max) => (value - max) * (value - min) <= 0.0;

        IDictionary<int, Fields> IdeologyDictionary = new Dictionary<int, Fields>();

        IdeologyDictionary.Add(0,
            new Fields(-1, 0.72, -0.72, 1.0, "Stalinism",
                "Stalinism is the means of governing and Marxist-Leninist policies implemented in the Soviet Union from 1927 to 1953 by Joseph Stalin."));
        IdeologyDictionary.Add(1,
            new Fields(-0.71, 0.72, -0.44, 1, "Maoism",
                "Maoism, officially called Mao Zedong Thought by the Chinese Communist Party, is a variety of Marxism–Leninism that Mao Zedong developed for realising a socialist revolution in the agricultural, pre-industrial society of the Republic of China and later the People's Republic of China."));
        IdeologyDictionary.Add(2,
            new Fields(-0.43, 0.72, -0.16, 1, "Juche",
                "Juche is the state ideology of North Korea, described by the country's government as 'Kim Il-sung's original, brilliant and revolutionary contribution to national and international thought'.It postulates that 'man is the master of his destiny', that the masses are to act as the 'masters of the revolution and construction', and that by becoming self-reliant and strong, a nation can achieve true socialism"));
        IdeologyDictionary.Add(3,
            new Fields(-0.14, 0.72, 0.14, 1.0, "National Socialism",
                "National Socialism, sometimes known as its clippings Nazsoc, Natsoc, and Nazi, but more commonly called Nazism, is a culturally far-right,  totalitarian and usually economically third-positionist ideology. However, their factions, which the mainstream Nazis eventually purged, had much more socialist views. "));
        IdeologyDictionary.Add(4,
            new Fields(0.14, 0.72, 0.42, 1.0, "Fascism",
                "Fascism is a totalitarian, culturally far-right, palingenetic ultranationalist, and third-positionist ideology. Fascism originates from the teachings of Giovanni Gentile and Benito Mussolini, originally outlined in The Doctrine of Fascism. Fascism has had many iterations throughout history which have built upon Mussolini's and Gentile's ideas, while the form of fascism that does not built off of it, but rather follows the original doctrine is called Italian Fascism or Classical Fascism. "));
        IdeologyDictionary.Add(5,
            new Fields(0.43, 0.72, 0.71, 1.0, "Falangism",
                "Falangism, is an economically third position (but usually left-leaning), culturally far right-wing,  ethnonationalist (but highly supports race mixing, claiming that the intermixing of the Spanish race and other races has produced a Hispanic supercaste that is ethically improved, morally robust, spiritually vigorous) authoritarian and nationalist ideology originating from Spain. It places a large emphasis on hierarchy, authority and order, and is opposed to communism and liberal democracy. Falangism believe in uniting all of Spain using violence, thus it heavily opposes regional Separatist movements. It believes in a form of  pan-Hispanic nationalism known as Hispinidad. It also believes in strict adherence to traditional gender roles."));
        IdeologyDictionary.Add(6,
            new Fields(0.72, 0.72, 1.00, 1.0, "Absolute Monarchy",
                "Absolute Monarchism (AbMon), or Absolutism, is an authoritarian ideology which advocates for a single monarch with absolute rule over the state who is not bound by any formal rules, often being seen as owning the countries they reside over. Such rule is often justified through  religious means though some modern proponents of this ideology use secular pragmatists arguments"));
        IdeologyDictionary.Add(7,
            new Fields(-1, 0.43, -0.72, 0.71, "Marxism-Leninism",
                "Marxism–Leninism is an authoritarian, economically far left ideology that can range from culturally left to culturally right (But usually culturally left). It is based on the Joseph Stalin's ideological synthesis of Orthodox Marxism and Leninism, although many self-proclaimed Marxist-Leninist leaders were opposed to Joseph Stalin, such as Fidel Castro and Nikita Khrushchev."));
        IdeologyDictionary.Add(8,
            new Fields(-0.71, 0.43, -0.44, 0.71, "Batthism",
                "Ba'athism is an Arab nationalist ideology which promotes the creation and development of a unified Arab state through the leadership of a vanguard party over a progressive revolutionary government."));
        IdeologyDictionary.Add(9,
            new Fields(-0.43, 0.43, -0.16, 0.71, "State Socialism",
                "State socialism is a political and economic ideology within the socialist movement advocating state ownership of the means of production, either as a temporary measure or as a characteristic of socialism in the transition from the capitalist to the socialist mode of production or communist society"));
        IdeologyDictionary.Add(10,
            new Fields(-0.14, 0.43, 0.14, 0.71, "Integralism",
                "Integralism, is a Statist, Culturally Right and Economically ambiguous ideology.Integralism broadly believes that there should be a  fully integrated social and political order, based on converging patrimonial (inherited) political, cultural, religious, and national traditions of a particular state Integralism is a deeply  Traditionalist and  Reactionary doctrine which rejects separation of Church and State, liberal and egalitarian values of the Enlightenment, and which believes that the state should submit to spiritual authorities (an example of such a scheme would be the Catholic Gelasian Diarchy)."));
        IdeologyDictionary.Add(11,
            new Fields(0.14, 0.43, 0.42, 0.71, "State Capitalism",
                "State Capitalism (StateCap) is an economic system in which the state takes an active role within commercial activity through the use of state-owned or state-managed for-profit enterprises. State capitalism can be partial (meaning that a state-owned co-exist with private ones) or full in which it is called state monopoly capitalism (StateMoCap). State capitalism has been classified as both economically right-wing (as the means of production don't belong to the working class) and left-wing (as it is collectivist in nature). "));
        IdeologyDictionary.Add(12,
            new Fields(0.43, 0.43, 0.71, 0.71, "Feudalism",
                "Feudalism is an authoritarian, decentralized, economically and culturally right-wing ideology practiced throughout Europe from as early as the 5th century to as late as technically 2008 (Channel Island of Sark) although its prime was from the 9th century- when the middle ages properly kicked off- to the 17th century- when  Enlightenment had properly started to create an entirely new status quo."));
        IdeologyDictionary.Add(13,
            new Fields(0.72, 0.43, 1.00, 0.71, "National Capitalism",
                "National Capitalism, often referred to by the abbreviations NazCap or NatCap[1] is a totalitarian, economically and culturally far-right, nationalist and fascist[2] ideology that combines  National Socialist cultural, civil and diplomatic principles with free market or laissez-faire capitalism, making it an even more extreme version of Pinochetism (whose authoritarianism is based primarily on dealing with political opposition). He inhabits the top right corner of the political compass, with  Pinochetism coming closest to him"));
        IdeologyDictionary.Add(14,
            new Fields(-1, 0.14, -0.72, 0.42, "Trotskyism",
                "Trotskyism is the political ideology and branch of Marxism developed by Ukrainian-Russian revolutionary Leon Trotsky and by some other members of the Left Opposition and Fourth International."));
        IdeologyDictionary.Add(15,
            new Fields(-0.71, 0.14, -0.44, 0.42, "Longism",
                "The political views of Huey P. Long have presented historians and biographers with some difficulty.While most say that Louisiana Governor and Senator Huey Long was a populist, little else can be agreed on. Huey Long's opponents, both during his life and after, often drew connections between him and his ideology and far-left and right political movements, comparing it to everything from European Fascism, Stalinism, and later McCarthyism."));
        IdeologyDictionary.Add(16,
            new Fields(-0.43, 0.14, -0.16, 0.42, "Social Liberalism",
                "also known as new liberalism, modern liberalism, is a political philosophy and variety of liberalism that endorses a social market economy within an individualist economy and the expansion of civil and political rights. Under social liberalism, the common good is viewed as harmonious with the freedom of the individual."));
        IdeologyDictionary.Add(17,
            new Fields(-0.14, 0.14, 0.14, 0.42, "Third Way",
                "Third Way is an economically centre to centre-right, civically moderate and culturally centre-left to left-wing ideology inhabiting the centre square. It is the child of Neoliberalism and Social Liberalism, borrowing elements from both. As with most centrist balls, it seeks to find a compromise between different political positions. "));
        IdeologyDictionary.Add(18,
            new Fields(0.14, 0.14, 0.42, 0.42, "Civil Libertarianism",
                "Civil Libertarianism is civically libertarian, economically variable and usually progressive ideology. It is a form of liberalism and libertarianism who lives in the bottom of the political compass - he believes in the expansion and preservation of civil liberties. Distinct from right libertarianism because he is focused on fighting for a legal system centered around the harm principle, privacy and civil rights without any interest in economic liberalism. Sometimes he can be portrayed as a scholar with nerdy expertise of the law and philosophy. He is also not to be confused with Social Libertarianism, who has similar policies."));
        IdeologyDictionary.Add(19,
            new Fields(0.43, 0.14, 0.71, 0.42, "Elective Monarchy",
                "Elective Monarchism, shortened to Elecmon, is a non-quadrant monarchist ideology. Elecmon was born as an alternative to lineage based monarchies who had a tendency to result in succession crisis after a few generations. The voting powers are usually only held by high ranking nobles and influential clergy members. "));
        IdeologyDictionary.Add(20,
            new Fields(0.72, 0.14, 1.00, 0.42, "Corporatism",
                "Corporatism is a economically third position, authoritarian ideology that advocates for the organization of society in different areas of employment like agriculture, military,  engineering etc called corporations which the government assigns people to and your employment into these is designated by your interests and skills, then the government sets a goal like for example the production of 40,000 cars per month with the expectation the standard is met. He believes in a cross-class system of regulation, in which the workers, employers, and state negotiate with one another in order to most efficiently run the economy and satisfy (in theory) all parties involved. "));
        IdeologyDictionary.Add(21,
            new Fields(-1, -0.13, -0.72, 0.13, "Posadism",
                "The Fourth International Posadist is a Trotskyist international. It was founded in 1962 by J. Posadas, who had been the leader of the Latin America Bureau of the Fourth International in the 1950s, and of the Fourth International's section in Argentina."));
        IdeologyDictionary.Add(22,
            new Fields(-0.71, -0.13, -0.44, 0.13, "Social Democracy",
                "Social democracy is a political, social, and economic philosophy within socialism that supports political and economic democracy. As a policy regime, it is described by academics as advocating economic and social interventions to promote social justice within the framework of a liberal-democratic polity and a capitalist-oriented mixed economy"));
        IdeologyDictionary.Add(23,
            new Fields(-0.43, -0.13, -0.16, 0.13, "Liberalism",
                "Liberalism is a political and moral philosophy based on the rights of the individual, liberty, consent of the governed and equality before the law.Liberals espouse a wide array of views depending on their understanding of these principles, but they generally support individual rights."));
        IdeologyDictionary.Add(24,
            new Fields(-0.14, -0.13, 0.14, 0.13, "Centrism",
                "They believe the best solution to all problems is an exact compromise between two sides. "));
        IdeologyDictionary.Add(25,
            new Fields(0.14, -0.13, 0.42, 0.13, "Neo-Libertarianism",
                "Neolibertarianism is an economically right-wing, center-libertarian, and culturally-variable, albeit usually, right-leaning ideology. His beliefs stem from the concept that negative rights are largely incompatible with a strictly limited government, and thus, he is generally supportive of government involvement in society as long as it promotes greater liberty. The most generally distinguished of these policies Neobert believes promote greater liberty are foreign military interventions, which puts him at odds with the majority of the people within his quadrant and very friendly to Neoconservatism. On this wiki, he is used to represent the ideology of conservative commentator Ben Shapiro, over large disagreement on whether he's a  Libertarian Conservative (which he self-describes as), or a Neoconservative (for his strong support of American foreign interventionism). "));
        IdeologyDictionary.Add(26,
            new Fields(0.43, -0.13, 0.71, 0.13, "Liberal Conservatism",
                "Liberal Conservatism, often shortened to LibCon is an economically centre-right to right, culturally moderate but generally right-leaning and usually centre to authoritarian-leaning on the lib-auth axis, ideology. Liberal conservatism incorporates the  Classical Liberal view of minimal government intervention in the economy, according to which individuals should be free to participate in the market and generate wealth without government interference. However, unlike most liberals, liberal conservatism emphasizes on trying to conserve this worldview and views any attempts to challenge it as an existential threat. They are also unafraid to be called conservative, even if they're often more socially liberal on certain issues. "));
        IdeologyDictionary.Add(27,
            new Fields(0.72, -0.13, 1.00, 0.13, "Capitalism",
                "Capitalism is an ideology and economic system representing a broad range of ideologies that fall under the umbrella of capitalism. Capitalism is defined in many different ways, by  socialists it's usually defined as a system where the bourgeoisie exploit the proletariat, by  market anarchists it's usually defined as  corrupt markets and by  Right-Libertarianism it's usually defined as a free enterprise system."));
        IdeologyDictionary.Add(28,
            new Fields(-1, -0.42, -0.72, -0.14, "Luxenburgism",
                "Rosa Luxenburg was a Polish and naturalised-German revolutionary socialist, Marxist philosopher and anti-war activist. Successively, she was a member of the Proletariat party, the Social Democracy of the Kingdom of Poland and Lithuania, the Communist Party of Germany (KPD), and many more. "));
        IdeologyDictionary.Add(29,
            new Fields(-0.71, -0.42, -0.44, -0.14, "Democratic Socialism",
                "Democratic socialism is a political philosophy that supports political democracy and some form of a socially owned economy,with a particular emphasis on economic democracy, workplace democracy, and workers' self-management within a market socialist economy, or an alternative form of decentralised planned socialist economy."));
        IdeologyDictionary.Add(30,
            new Fields(-0.43, -00.42, -0.16, -0.14, "Liberal Democracy",
                "Liberal democracy is the combination of a liberal political ideology that operates under an indirect democratic form of government. It is characterised by elections between multiple distinct political parties, a separation of powers into different branches of government, the rule of law in everyday life as part of an open society, a market economy with private property, and the equal protection of human rights, civil rights, civil liberties and political freedoms for all people."));
        IdeologyDictionary.Add(31,
            new Fields(-0.14, -0.42, 0.14, -0.14, "Welfare Capitalism",
                "Social Capitalism, shortened to SocCap also known as Rhine Capitalism, Welfare Capitalism and Social market economy, is an economically centre to centre-right ideology which combines stances from both Social Democracy and Capitalism. "));
        IdeologyDictionary.Add(32,
            new Fields(0.14, -0.42, 0.42, -0.14, "Classical Liberalism",
                "Classical Liberalism is an economically center-right to far-right,  mildly to moderately libertarian, and  culturally variable ideology. "));
        IdeologyDictionary.Add(33,
            new Fields(0.43, -0.42, 0.71, -0.14, "Georgism",
                "Georgism, also called Geoism, is an economically centrist, generally culturally center-left, and moderately libertarian ideology that is typically placed in the lower middle of the political compass. "));
        IdeologyDictionary.Add(34,
            new Fields(0.72, -0.42, 1.0, -0.14, "Libertarianism",
                "Libertarianism, or more simply Right-Libertarianism or Libertarian Capitalism, is a civically libertarian, laissez-faire capitalist and culturally variable ideology. He inhabits the  libertarian right quadrant of the political compass, generally being in the middle of it unless specified.He believes in a very limited government and the individual's natural self evident rights of life, liberty, and property. He likes the use of militias to watch them.He technically believes in the same principles of classical liberalism of equality before the law and the basic rights to life, liberty, and property, along with most librights, although some people debate most libertarians are only libertarians because of the precise ideology and not the principles of it."));
        IdeologyDictionary.Add(35,
            new Fields(-1, -0.71, -0.72, -0.43, "Council Communism",
                "Council communism is a current of communist thought that emerged in the 1920s. Inspired by the November Revolution, council communism was opposed to state socialism and advocated workers' councils and council democracy. Strong in Germany and the Netherlands during the 1920s, council communism continues to exist as a small minority in the left."));
        IdeologyDictionary.Add(36,
            new Fields(-0.71, -0.71, -0.44, -0.43, "Progressivism",
                "Progressivism is a political philosophy in support of social reform.Based on the idea of progress in which advancements in science, technology, economic development and social organization are vital to the improvement of the human condition, progressivism became highly significant during the Age of Enlightenment in Europe"));
        IdeologyDictionary.Add(37,
            new Fields(-0.43, -0.71, -0.16, -0.43, "Libertarian Socialism",
                "Libertarian Socialism  is an anti-authoritarian, anti-statist and libertarian[9][10] political philosophy within the socialist movement which rejects the state socialist conception of socialism as a statist form where the state retains centralized control of the economy."));
        IdeologyDictionary.Add(38,
            new Fields(-0.14, -0.71, 0.14, -0.43, "Minarchism",
                "Minarchism is a political ideology which seeks to establish a form of governance in which state intervention is as minimal as possible while still maintaining a broadly functional society of which the general standard for functional varies from person to person. This form of governance is called a Minarchy or a Night-Watchman State. "));
        IdeologyDictionary.Add(39,
            new Fields(0.14, -0.71, 0.42, -0.43, "Geolibertarianism",
                "Geolibertarianism, shortened to Geolib, is an economically Center-right to right, culturally variable, and anti-authoritarian ideology that believes in an un-tampered free market save for a singular land-value tax"));
        IdeologyDictionary.Add(40,
            new Fields(0.43, -0.71, 0.71, -0.43, "Objectivism",
                "Objectivism also known as Randism or Randianism is a philosophy of author and philosopher Ayn Rand. On political matters, objectivists favour free market capitalism and the opposition to statism. "));
        IdeologyDictionary.Add(41,
            new Fields(0.72, -0.71, 1.0, -0.43, "Paleolibertarianism",
                "Paleolibertarianism (shortened to Paleobert and Paleolib) also called Old Right Libertarianism is a type of Libertarian sub-ideology which stresses inherent incompatibility between cultural egalitarianism/progressivism, and inclusionarism, and the concept of liberty, as well as a focus on the importance of inherited culture as a means of maintaining order.Paleolibertarianism is distinguished by a particular opposition to all types of state interventionism, especially  Foreign and  Economic. Paleolibertarianism as an ideology is one which embodies a culturally and economically right-wing and a civically libertarian position."));
        IdeologyDictionary.Add(42,
            new Fields(-1, -1, -0.72, -0.72, "Anarcho-communism",
                "Anarcho-communism,also known as anarchist communism,is a political philosophy and anarchist school of thought which advocates the abolition of the state, capitalism, wage labour, social hierarchies and private property."));
        IdeologyDictionary.Add(43,
            new Fields(-0.71, -1, -0.44, -0.72, "Anarcho-Collectivism",
                "Anarcho-Collectivism is a revolutionary socialist doctrine and anarchist school of thought that advocates the abolition of both the state and private ownership of the means of production, as it envisions the means of production instead being owned collectively, whilst controlled and self-managed by the producers and workers themselves."));
        IdeologyDictionary.Add(44,
            new Fields(-0.71, -1, -0.44, -0.72, "Anarcho-Collectivism",
                "Anarcho-Collectivism is a revolutionary socialist doctrine and anarchist school of thought that advocates the abolition of both the state and private ownership of the means of production, as it envisions the means of production instead being owned collectively, whilst controlled and self-managed by the producers and workers themselves."));
        IdeologyDictionary.Add(45,
            new Fields(-0.14, -1.0, 0.14, -0.72, "Agorism",
                "Agorism, shortened to A3 (Anarchy, Agora, Action), is an Anarchist and economically right-wing ideology.Agorism as a political ideology is more about the means than the ends. It, in short, states that the best way to achieve a free society is peaceful, through the utilization of black and grey markets. It was first proposed by Samuel Edward Konkin III at the conferences CounterCon I in October 1974 and in CounterCon II in May 1975."));
        IdeologyDictionary.Add(46,
            new Fields(0.14, -1.0, 0.42, -0.72, "Anarcho Mutualism",
                " Anarcho-Mutualism (AnMut), is an economically left-wing (but pro-market), Anarchist and culturally variable ideology primarily based upon the writings of Pierre-Joseph Proudhon. "));
        IdeologyDictionary.Add(47,
            new Fields(0.43, -1.0, 0.71, -0.72, "Hoppeanism",
                "Hoppeanism, sometimes also referred to as Conservative Anarcho-Capitalism, is a Culturally right-wing tendency within  Anarcho-Capitalism which puts emphasis on the importance of exclusionary behaviour (ostracism), communitarianism, social conservatism (and its compatibility and complementation with libertarianism), and the opposition to  Democracy if one is to maintain the continuous existence of the  libertarian social order."));
        IdeologyDictionary.Add(48,
            new Fields(0.72, -1.0, 1.0, -0.72, "Anarcho-Capitalism",
                "Anarcho-Capitalism (AnCap), also called Private Property Anarchy, Private Law Society,[5] and Rothbardianism,[6] as well a bunch of other names,[7] is a political ideology, as well as a theoretical social order, based around Classical Liberal conception of  property rights, individualism, and rejection of the state but lead to its logical conclusion, the elimination of it."));

        return (from Fields value in IdeologyDictionary.Values
            where InRange(x,
                      value.StartX,
                      value.EndX) &&
                  InRange(y,
                      value.StartY,
                      value.EndY)
            select new IdeologyInfo(value.IdeologyName,
                value.IdeologyDescription)).FirstOrDefault();
    }
}
