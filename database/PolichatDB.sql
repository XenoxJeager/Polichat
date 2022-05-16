DROP DATABASE IF EXISTS polichat;
CREATE DATABASE polichat;
USE polichat;

CREATE TABLE IF NOT EXISTS Questions (
    Text CHAR(255),
    Type CHAR(255),
    P_Strongly_Agree_X DOUBLE(16,2),
    P_Strongly_Agree_Y DOUBLE(16,2),
    P_Agree_X DOUBLE(16,2),
    P_Agree_Y DOUBLE(16,2),
    P_Disagree_X DOUBLE(16,2),
    P_Disagree_Y DOUBLE(16,2),
    P_Strongly_Disagree_X DOUBLE(16,2),
    P_Strongly_Disagree_Y DOUBLE(16,2),
    PRIMARY KEY(Text)
);

INSERT INTO QUESTIONS (Text, Type, P_Strongly_Agree_X,P_Strongly_Agree_Y,P_Agree_X,P_Agree_Y,P_Disagree_X,P_Disagree_Y,P_Strongly_Disagree_X,P_Strongly_Disagree_Y)VALUES
#security/freedom 
('If you have nothing to hide, then you should not care about the government having access to your communications. ','security/freedom',0.05,0.2,0.024,0.12,-0.04,-0.12,-0.06,-0.19),
('Government surveillance is necessary in the modern world.','security/freedom',0.0,0.25,0.0,0.14,0.0,-0.15,0.0,-0.2),
('The government should monitor all citizens to combat terrorism.','security/freedom',0.03,0.26,0.02,0.14,-0.03,-0.12,-0.02,-0.2),
('Whistleblowers should be strongly protected.','security/freedom',0.0,0.3,0.0,0.16,0.0,-0.13,0.0,-0.27),
('The very existence of the state is a threat to our liberty.','security/freedom',0.0,0.4,0.0,0.2,0.0,0.0,-0.11,-0.22),
#federal/unitary 
('Local governments address issues that the national government would never touch.','federal/unitary',0.0,0.2,0.0,0.09,0.0,-0.07,0.0,-0.17),
('A united world government would be beneficial to mankind.','federal/unitary',-0.3,-0.05,-0.12,-0.01,0.07,0.09,0.13,0.15),
('Local governments give each region good representation of their views.','federal/unitary',-0.01,-0.24,0.0,0.13,0.0,0.11,0.02,0.25),
('In order for humanity to survive, we must get past having separate nations.','federal/unitary',-0.09,-0.18,-0.04,-0.08,0.06,0.08,0.09,0.13),
('Research should be conducted on an international scale.','federal/unitary',-0.05,-0.03,-0.02,-0.01,0.02,0.03,0.09,0.14),
#democracy/authority 
('Each person should have one vote, each equal to every other.','democracy/authority',-0.28,-0.22,-0.18,-0.08,0.04,0.09,0.15,0.19),
('Elections are a waste of resources.','democracy/authority',0.24,0.32,0.11,0.17,-0.13,-0.16,-0.18,-0.25),
('The stronger the leadership, the better.','democracy/authority',0.0,0.33,0.0,0.17,0.0,-0.18,0.0,-0.35),
('It is more important to retain peaceful relations than to further our strength.','democracy/authority',-0.24,-0.22,-0.15,-0.11,0.11,0.13,0.16,0.19),
('Oppression by corporations is more of a concern than oppression by governments.','democracy/authority',0.23,0.21,0.13,0.15,-0.12,-0.15,-0.21,-0.26),
#militarist/pacifist Y
('It is important to maintain our national sovereignty.','militarist/pacifist',0.24,0.19,0.14,0.11,-0.12,-0.15,-0.23,-0.27),
('A national government must be strong to adequately protect all its citizens.','militarist/pacifist',0.04,0.32,0.02,0.19,0.0,-0.13,0.0,-0.23),
('A nations culture is important to protect.','militarist/pacifist',0.21,0.13,0.13,0.03,-0.13,-0.17,-0.18,-0.23),
('Peace is preferable to war whenever possible.','militarist/pacifist',-0.41,-0.25,-0.23,-0.13,0.13,0.19,0.17,0.23),
('Border protection is important.','militarist/pacifist',0.0,0.21,0.0,0.13,0.0,-0.14,0.0,-0.19),
#globalist/isolationist X 
('Nobody in other countries has our best interests in mind.','globalist/isolationist',0.32,0.18,0.28,0.14,-0.12,-0.15,-0.15,-0.21),
('No cultures are superior to others.','globalist/isolationist',-0.32,-0.25,-0.16,-0.17,0.13,0.18,0.14,0.29),
('International aid is a waste of money.','globalist/isolationist',0.13,0.19,0.11,0.11,-0.11,-0.13,-0.17,-0.19),
('My nation is closer to my views than most nations in the world.','globalist/isolationist',0.1,0.1,0.08,0.07,-0.07,-0.06,-0.11,-0.08),
('Governments should be as concerned about foreign citizens as they are about those within their borders.','globalist/isolationist',-0.21,-0.17,-0.17,-0.13,0.13,0.15,0.19,0.23),
#economic X
('The government should not break up monopolies.','economic', 0.3, -0.1, 0.2, -0.1, -0.3, 0.1, -0.2, 0.1),
('From each according to his ability, to each according to his need.','economic', -0.3, 0, -0.15, 0, 0.15, 0, 0.3, 0),
('It is better to maintain a balanced budget than to ensure welfare for all citizens.','economic', 0.2, 0, 0.1, 0, -0.1, 0, -0.2, 0),
('Economies without any capitalism will collapse.','economic', 0.3, 0, 0.2, 0, -0.2, 0, -0.3, 0),
('People should have the right to leave their wealth to their descendents.','economic', 0.2, 0, 0.1, 0, -0.1, 0, -0.2, 0),
#atheist/religious X
('Religion should be banned.','religious', -0.15, 0.2, -0.05, 0.1, 0.1, -0.05, 0.2, -0.1),
('Most bad things happening in the world are caused by us turning away from religion.','religious', 0.3, 0, 0.2, 0, -0.2, 0, -0.3, 0),
('There is no higher power.','religious', 0.2, 0, 0.1, 0, -0.1, 0, -0.2, 0),
('Religion must decline for society to progress.','religious', -0.2, 0, -0.1, 0, 0.1, 0, 0.2, 0),
('My religious values should be spread as much as possible.','religious', 0.3, 0.2, 0.15, 0.1, -0.15, -0.1, -0.3, -0.2),
#progress/tradition X
('Sex outside marriage is immoral.','tradition', 0.3, 0.3, 0.2, 0.2, -0.05, -0.1, -0.05, -0.2),
('Traditional medicines are often more effective than modern medicines.', 'tradition', 0.1, 0.2, 0.05, 0.1, -0.1, 0, -0.15, 0),
('Laws should not be based on religion.','tradition', -0.2, 0, -0.1, 0, 0.1, 0, 0.2, 0),
('Genetic modification should be used rarely, if ever.','tradition', 0, -0.2, 0, -0.1, 0, 0.1, 0, 0.2),
('Abortion should be legal in all cases.','tradition', -0.3, -0.1, -0.2, -0.05, 0.1, 0.05, 0.3, 0.2);