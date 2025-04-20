import React, { useState, useRef, useEffect } from "react";
import '../css/DungeonBook.css';
import logo from '/logo.jpg';
import pages from '../../pages.json';

const DungeonBook = () => {
    const [currentSpread, setCurrentSpread] = useState(0);
    const [isOpen, setIsOpen] = useState(false);
    const [isMobile, setIsMobile] = useState(false);
    const bookRef = useRef(null);

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth <= 768);
        };

        handleResize();
        window.addEventListener('resize', handleResize);

        return () => window.removeEventListener('resize', handleResize);
    }, []);

    const totalSpreads = Math.ceil((pages.length + 1) / 2);

    const goToPreviousSpread = () => {
        if (currentSpread > 0) {
            setCurrentSpread(currentSpread - 1);
        }
    };

    const goToNextSpread = () => {
        if (currentSpread < totalSpreads - 1) {
            setCurrentSpread(currentSpread + 1);
        }
    };

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

    const toggleBook = () => {
        if (isOpen) {
            setIsOpen(false);
            setTimeout(() => {
                setCurrentSpread(0);
            }, 600); // A zárási animáció időtartama megmarad
        } else {
            setCurrentSpread(0);
            setIsOpen(true);
        }
    };

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

    const EbookView = () => {
        const [currentPage, setCurrentPage] = useState(0);
        const [touchStart, setTouchStart] = useState(null);
        const [touchEnd, setTouchEnd] = useState(null);
        const ebookRef = useRef(null);
    
        const allPages = [{ isLogo: true }, ...pages];
        const minSwipeDistance = 50;
    
        const onTouchStart = (e) => {
            e.stopPropagation();
            setTouchEnd(null);
            setTouchStart(e.targetTouches[0].clientX);
            document.body.style.overflow = 'hidden';
        };
    
        const onTouchMove = (e) => {
            e.stopPropagation();
            setTouchEnd(e.targetTouches[0].clientX);
        };
    
        const onTouchEnd = (e) => {
            e.stopPropagation();
            if (!touchStart || !touchEnd) return;
            const distance = touchStart - touchEnd;
            const isLeftSwipe = distance > minSwipeDistance;
            const isRightSwipe = distance < -minSwipeDistance;
    
            if (isLeftSwipe && currentPage < allPages.length - 1) {
                setCurrentPage(currentPage + 1);
            }
            if (isRightSwipe && currentPage > 0) {
                setCurrentPage(currentPage - 1);
            }
            document.body.style.overflow = 'auto';
        };
    
        return (
            <div className="ebook-container">
                <div className="ebook-book" ref={ebookRef}>
                    <div
                        className="ebook-page"
                        onTouchStart={onTouchStart}
                        onTouchMove={onTouchMove}
                        onTouchEnd={onTouchEnd}
                    >
                        {allPages[currentPage] ? (
                            allPages[currentPage].isLogo ? (
                                <div className="ebook-content-wrapper">
                                    <div className="logo-container">
                                        <img
                                            src={logo}
                                            alt="Dungeon Valley Explorer Logo"
                                            className="book-logo"
                                        />
                                    </div>
                                    <div className="page-content">
                                        <h2>Welcome to Dungeon Valley Explorer</h2>
                                        <p>Your journey begins here.</p>
                                    </div>
                                </div>
                            ) : (
                                <div className="page-content">
                                    {allPages[currentPage].title && <h2>{allPages[currentPage].title}</h2>}
                                    <p>{allPages[currentPage].content}</p>
                                </div>
                            )
                        ) : (
                            <div className="empty-page" />
                        )}
                    </div>
                </div>
                
                
            </div>
        );
    };

    const BookView = () => {
        // Eltávolítjuk az isFlipping állapotot, mert nincs animáció
        const goToPreviousSpread = () => {
            if (currentSpread > 0) {
                setCurrentSpread(currentSpread - 1); // Azonnali váltás
            }
        };

        const goToNextSpread = () => {
            if (currentSpread < totalSpreads - 1) {
                setCurrentSpread(currentSpread + 1);
            }
        };

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

    return isMobile ? <EbookView /> : <BookView />;
};

export default DungeonBook;