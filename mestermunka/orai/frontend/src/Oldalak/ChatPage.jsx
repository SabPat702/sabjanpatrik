import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/ChatPage.css';

const ChatPage = () => {
    const [posts, setPosts] = useState([]);
    const [newPost, setNewPost] = useState('');
    const [loading, setLoading] = useState(false);
    const username = localStorage.getItem('username') || 'Vendég';
    const navigate = useNavigate();
    const postsEndRef = useRef(null); // Referencia az utolsó bejegyzéshez

    // Automatikus görgetés az új üzenetekhez
    const scrollToBottom = () => {
        postsEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    };

    const fetchPosts = async () => {
        setLoading(true);
        try {
            const res = await fetch('http://localhost:3001/chat');
            const data = await res.json();
            setPosts(data);
        } catch (error) {
            console.error('Hiba a bejegyzések lekérésekor:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    useEffect(() => {
        scrollToBottom(); // Görgetés az új üzenetekhez, ha új bejegyzés érkezik
    }, [posts]);

    const handlePost = async () => {
        if (!newPost.trim()) return;

        try {
            const res = await fetch('http://localhost:3001/chat', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ author: username, text: newPost }),
            });

            if (res.ok) {
                setNewPost('');
                fetchPosts();
            }
        } catch (error) {
            console.error('Hiba a bejegyzés küldésekor:', error);
        }
    };

    const handleReport = async (postId) => {
        try {
            await fetch(`http://localhost:3001/chat/${postId}/report`, {
                method: 'POST',
            });
            alert('Bejegyzés jelentve!');
        } catch (error) {
            console.error('Hiba a jelentés során:', error);
        }
    };

    return (
        <div className="chat-page">
            <nav className="chat-nav">
                <span className="nav-title" onClick={() => navigate('/DungeonValleyExplorer')}>
                    🏠 Dungeon Valley Explorer
                </span>
                <span className="nav-center">💬 Chat Szoba</span>
            </nav>

            <div className="posts">
                {loading ? (
                    <p style={{ textAlign: 'center', color: '#666' }}>Betöltés...</p>
                ) : posts.length === 0 ? (
                    <p style={{ textAlign: 'center', color: '#666' }}>
                        Még nincsenek bejegyzések. Legyél az első, aki ír!
                    </p>
                ) : (
                    posts.map((post) => (
                        <div key={post.id} className="post">
                            <p>
                                <strong>{post.author}</strong>: {post.text}
                            </p>
                            <button onClick={() => handleReport(post.id)}>Report</button>
                        </div>
                    ))
                )}
                <div ref={postsEndRef} /> {/* Referencia az utolsó bejegyzéshez */}
            </div>

            <div className="post-form">
                <h3>Írjon egy bejegyzést</h3>
                <textarea
                    value={newPost}
                    onChange={(e) => setNewPost(e.target.value)}
                    placeholder="Írjon valamit..."
                    maxLength={500} // Karakterkorlát a visszaélések elkerülésére
                />
                <button onClick={handlePost}>Küldés</button>
            </div>
        </div>
    );
};

export default ChatPage;