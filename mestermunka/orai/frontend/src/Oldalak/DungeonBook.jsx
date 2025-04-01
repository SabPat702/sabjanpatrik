import React, { useState, useRef } from "react";
import '../css/DungeonBook.css';
import logo from '/logo.jpg';
import pages from '../../pages.json';

const DungeonBook = () => {
    const [currentSpread, setCurrentSpread] = useState(0);
    const [isOpen, setIsOpen] = useState(false);
    const bookRef = useRef(null);

    const totalSpreads = Math.ceil((pages.length + 1) / 2); // +1 a logós kezdőlap miatt

    // Lapozási logika egyszerűsítése
    const goToPreviousSpread = () => {
        if (isOpen && currentSpread > 0) {
            setCurrentSpread(currentSpread - 1);
        }
    };

    const goToNextSpread = () => {
        if (isOpen && currentSpread < totalSpreads - 1) {
            setCurrentSpread(currentSpread + 1);
        }
    };

    // Drag események (jelenleg csak logolnak, de később bővíthetők)
    const handleDragStart = (e) => {
        e.preventDefault();
        console.log("Drag started");
    };

    const handleDragMove = (e) => {
        e.preventDefault();
        console.log("Dragging...");
    };

    const handleDragEnd = (e) => {
        e.preventDefault();
        console.log("Drag ended");
    };

    // Könyv nyitása/zárása
    const toggleBook = () => {
        setIsOpen(!isOpen);
        if (!isOpen) setCurrentSpread(0); // Ha kinyitjuk, visszaugrunk az elejére
    };

    // Oldalak tartalmának meghatározása
    const getPageContent = () => {
        if (!isOpen) return { left: null, right: null };

        if (currentSpread === 0) {
            return {
                left: { isLogo: true },
                right: pages[0] || null
            };
        }

        const leftIndex = (currentSpread * 2) - 1;
        const rightIndex = leftIndex + 1;
        return {
            left: pages[leftIndex] || null,
            right: pages[rightIndex] || null
        };
    };

    const { left, right } = getPageContent();

    return (
        <div className="dungeon-book-container">
            <div
                className={`book ${isOpen ? 'open' : 'closed'}`}
                ref={bookRef}
                onMouseDown={handleDragStart}
                onMouseMove={handleDragMove}
                onMouseUp={handleDragEnd}
                onMouseLeave={handleDragEnd}
                onTouchStart={handleDragStart}
                onTouchMove={handleDragMove}
                onTouchEnd={handleDragEnd}
            >
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
                                            <img
                                                src={logo}
                                                alt="Dungeon Valley Explorer Logo"
                                                className="book-logo"
                                            />
                                        </div>
                                    ) : (
                                        <div className="page-content">
                                            {left.title && <h2>{left.title}</h2>}
                                            <p>{left.content}</p>
                                            <div
                                                className="page-corner page-corner-left"
                                                onClick={goToPreviousSpread}
                                                onTouchStart={goToPreviousSpread}
                                                role="button"
                                                aria-label="Previous page"
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
                                            onClick={goToNextSpread}
                                            onTouchStart={goToNextSpread}
                                            role="button"
                                            aria-label="Next page"
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
                <button onClick={toggleBook}>
                    {isOpen ? 'Close Book' : 'Open Book'}
                </button>
            </div>
        </div>
    );
};

export default DungeonBook;