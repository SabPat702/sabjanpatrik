import React, { useState, useRef } from "react";
import '../css/DungeonBook.css';
import logo from '/logo.jpg';

const DungeonBook = () => {
    const [currentSpread, setCurrentSpread] = useState(0);
    const [isOpen, setIsOpen] = useState(false);
    const bookRef = useRef(null);

    const pages = [
        {
            title: "Introduction Hero And NPC's",
            content: ` Dungeon Valley Explorer is a text-based, guided game. Dungeon Valley Explorer takes place in a fictional world filled with elements from the knightly age and various allusions, as well as different locations and battles, which are resolved through text-based options. There are various NPCs in different places who can be asked for help. However, there is also another way to seek assistance: the aids within the attack, which include the Skill and Magic sections.`
        },
        {
            title: "Hero:",
            content:
            `You are the main character that you control in the game, and here is what you should know about him: You can name your hero! The hero has physical protection, magical protection, health, ability, and experience (which you can accumulate through experience points). Of course, you also play as the hero, so you can track these attributes during the game. We receive different weapons and armor that can help us in battles. The hero’s abilities are not overlooked either. He has two types of abilities: One type is a specific skill, like better sword handling.`,            
        },
        {
            content:
            `The other type is a fantasy-based ability, such as a super-powerful blow or punch. The hero also has his own magic which can heal his life (Self-care/Heal). NPCs are important to the hero, as they assist him in several ways during the adventure. NPCs can give you small quests or special potions that provide temporary extra abilities. Each NPC has a name, a detailed description, health, physical protection, magical protection, and experience. NPCs (Non-Player Characters) also have different levels. They carry weapons and armor that they can use to help us. Like the hero, they each have their own abilities, and every NPC is unique! Some NPCs will join us as teammates and fight`,
        },      
        {
            content: `alongside us, but not all NPCs will do so. Some may only provide a charm or a small amount of help before we continue.`,
        },                    
        {
            title: "Monsters and the races",
            content:
                        `Races play a special role in the game, as they have many characteristics. Each race has a name and detailed description within the game. These races can deal lethal damage in the game, so be cautious around them. However, some races deal weaker damage, and while they still have resistance, their attacks are less dangerous. The damage levels are: neutralizing/null < endured/tolerant < resistant < weak < deadly/fatal. These are the levels of damage different races can deal.`
        },
        {
            content:
            `Monsters also play an important role in the game, as they are the enemies you fight against. Monsters have health, physical protection, and magical protection. Their offensive power varies, and they have abilities that they use during combat. Monsters also have their own species and behaviors, which influence how they act during combat. They appear in various locations with varying strengths! At this point, AI comes into play, determining the thinking and behavior of monsters. The AI must randomly choose one of the available behaviors for monsters in combat.`,
        },
        {
            title: "Dungeon and the Environmental Hazards",
            content:
                    `Environmental hazards are important factors in the game, as no one wants to lose health unnecessarily. Environmental hazards can include ravines, traps, thorny bushes, or lava. These are just a few examples of the dangers that can be encountered. Environmental hazards have attack power, damage types, critical attack chances, and attack multipliers. Some hazards also have special abilities, which are hidden in specific locations.`
        },
        {
            content:
                    `Dungeon exploration is a core part of the game, contributing to its complexity and difficulty. You can progress forward, backward, or even leave the dungeon during exploration. You may also decide to leave the dungeon if you choose to stop exploring. Going backward represents rest. If you move forward, you won’t know what you might encounter. Only after exploring all areas in the dungeon can you complete it. If you leave the dungeon, you retain experience and money, but any progress related to the mission will be lost, and you'll have to start over.`,
        },
                    {
                        content: `The rest feature can only be used once per dungeon, allowing you to regain health and magic points. However, resting does not guarantee safety, as you may lose money, items, or potions, or even be attacked. If you decide to retreat before resting, the risk of these events is reduced. However, enemies may reappear in previously discovered rooms.`,
                    },
        {
            title: "Skill and Magic",
            content:
                    `Skills are unique abilities in the game. These skills have a critical attack chance, which is enhanced by a multiplier. Each skill has special effects, and there is arange beyond which it cannot reach. Using these skills may come at a cost, and their reload times may be extended. Magic plays an important role in the game. It has its own detailed description, but there are a few key points to know. Magic has attack and damage values, and different types of magic exist. Magic can also perform critical attacks, with multipliers under certain circumstances. Magic has a range and cannot reach infinity, `,          
        },
        {
            content: `and it also has special effects. Finally, magic takes time to recharge, a process that can vary widely—some spells might refresh quickly, while others, particularly the most potent ones, could require a lengthy cooldown, demanding careful timing and resource management from the player`,
        },
        {
            title: "Consumables, Armor, Weapons, Special Effects, Passive, Buffs/Debuffs",
            content: ` There are detailed descriptions of the items you can use in the game, and some additional points worth noting. Items have special abilities, but they require in-game currency to use. You do not need to pay real money; instead, you use money earned in the game. Weapons are vital in the game. The strength of a weapon depends on its type and its ability to deal damage. Some weapons can deal critical damage, which can be amplified by a special multiplier.`,
        },
        {
            content: `Each weapon has a unique effect on opponents, and their range differs—swords and spears, for example, have different lengths. Weapons can be combined with abilities, creating powerful combinations. Weapons also cost in-game money. The game includes special effects that influence abilities. There are temporary effects, triggered by specific objects or situations, that may help or hinder your progress. Some effects are always active, making them less noticeable, but they can still have an impact on gameplay.`
        },
        {
            title: "The NPC's of the city",
            content: ` There are different NPCs located throughout the city. At the Blacksmith, you can upgrade your own and your teammates' armor and weaponry, or have new items made. However, the Blacksmith cannot make weapons or armor without limits. You will need to pay for these upgrades, and there are restrictions on how often they can be done. At the Alchemist, you can obtain various potions for yourself and your teammates. The Alchemist is initially closed and will only open at a later stage in the game. In the Pub, you can buy food and drinks from the`,
        },
        {
            content: `innkeeper, which provide temporary buffs to the hero or the whole team. You can only level up after resting at the Pub, but until then, your accumulated experience will not be utilized. At the Merchant, you can sell items such as potions or equipment, or find special items that are not available elsewhere. The Merchant’s business can be supported or improved by completing quests, allowing you to access a larger selection of items and even receive discounts. The Merchant is initially locked and will only become available after a later mission. There is a special place where heroes can change their team, replacing adventurers with others who fit into the game's story,  `,
        },
        {
            content: `or even recruit weaker adventurers, if the player doesn't want to use them or hasn't unlocked them as usable characters.`,
        }
    ];

    const totalSpreads = Math.ceil((pages.length + 1) / 2);

    // Bal sarokra kattintás: előző oldal
    const handleLeftCornerClick = (e) => {
        e.preventDefault();
        if (!isOpen || currentSpread === 0) return;
        setCurrentSpread(currentSpread - 1);
    };

    // Jobb sarokra kattintás: következő oldal
    const handleRightCornerClick = (e) => {
        e.preventDefault();
        if (!isOpen || currentSpread >= totalSpreads - 1) return;
        setCurrentSpread(currentSpread + 1);
    };

    const toggleBook = () => {
        setIsOpen(!isOpen);
    };

    const getPageContent = () => {
        if (currentSpread === 0) {
            return {
                left: { isLogo: true },
                right: pages[0]
            };
        } else {
            const leftIndex = (currentSpread * 2) - 1;
            const rightIndex = leftIndex + 1;
            return {
                left: pages[leftIndex] || null,
                right: pages[rightIndex] || null
            };
        }
    };

    const { left, right } = getPageContent();

    return (
        <div className="dungeon-book-container">
            <div className={`book ${isOpen ? 'open' : 'closed'}`} ref={bookRef}>
                <div className="book-inner">
                    {!isOpen ? (
                        <div className="book-cover">
                            <h1>Dungeon Valley Explorer</h1>
                            <p>A Text-Based Adventure</p>
                        </div>
                    ) : (
                        <>
                            <div className="page left-page">
                                {left ? (
                                    left.isLogo ? (
                                        <div className="logo-container">
                                            <img src={logo} alt="Dungeon Valley Explorer Logo" className="book-logo" />
                                        </div>
                                    ) : (
                                        <div className="page-content">
                                            {left.title && <h2>{left.title}</h2>}
                                            <p>{left.content}</p>
                                            <div
                                                className="page-corner page-corner-left"
                                                onClick={handleLeftCornerClick}
                                                onTouchStart={handleLeftCornerClick}
                                            />
                                        </div>
                                    )
                                ) : (
                                    <div className="empty-page" />
                                )}
                            </div>
                            <div className="page right-page">
                                {right ? (
                                    <div className="page-content">
                                        {right.title && <h2>{right.title}</h2>}
                                        <p>{right.content}</p>
                                        <div
                                            className="page-corner page-corner-right"
                                            onClick={handleRightCornerClick}
                                            onTouchStart={handleRightCornerClick}
                                        />
                                    </div>
                                ) : (
                                    <div className="empty-page" />
                                )}
                            </div>
                        </>
                    )}
                </div>
            </div>
            <div className="navigation">
                <button onClick={toggleBook}>{isOpen ? 'Close Book' : 'Open Book'}</button>
            </div>
        </div>
    );
};

export default DungeonBook;