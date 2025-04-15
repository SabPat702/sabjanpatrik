import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/ChatPage.css';

const ChatPage = () => {
    const [posts, setPosts] = useState([]);
    const [newPost, setNewPost] = useState('');
    const [loading, setLoading] = useState(false);
    const [editingPost, setEditingPost] = useState(null);
    const [menuOpenId, setMenuOpenId] = useState(null); // új: megnyitott menü azonosító
    const username = localStorage.getItem('username') || 'Vendég';
    const navigate = useNavigate();
    const postsEndRef = useRef(null);

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
        scrollToBottom();
    }, [posts]);

    const handlePost = async () => {
        if (!newPost.trim()) return;

        try {
            if (editingPost) {
                await fetch(`http://localhost:3001/chat/${editingPost.id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ text: newPost }),
                });
                setEditingPost(null);
            } else {
                await fetch('http://localhost:3001/chat', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ author: username, text: newPost }),
                });
            }

            setNewPost('');
            fetchPosts();
        } catch (error) {
            console.error('Hiba a bejegyzés küldésekor:', error);
        }
    };

    const handleDelete = async (postId) => {
        if (!window.confirm('Biztosan törlöd ezt a bejegyzést?')) return;
        try {
            await fetch(`http://localhost:3001/chat/${postId}`, {
                method: 'DELETE',
            });
            fetchPosts();
        } catch (error) {
            console.error('Hiba a törlés során:', error);
        }
    };

    const handleEdit = (post) => {
        setEditingPost(post);
        setNewPost(post.text);
        setMenuOpenId(null); // Menü becsukása
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

    const toggleMenu = (postId) => {
        setMenuOpenId((prevId) => (prevId === postId ? null : postId));
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
                            <div className="post-content-wrapper">
                                <p>
                                    <strong>{post.author}</strong>: {post.text}
                                </p>
                                {post.author === username && (
                                    <div className="post-menu-wrapper">
                                        <button className="post-menu-button" onClick={() => toggleMenu(post.id)}>
                                            ⋮
                                        </button>
                                        {menuOpenId === post.id && (
                                            <div className="post-dropdown">
                                                <div onClick={() => handleEdit(post)}>✏️ Szerkesztés</div>
                                                <div onClick={() => handleDelete(post.id)}>🗑️ Törlés</div>
                                                 
                                            </div>
                                        )}
                                    </div>
                                )}
                            </div>
                        </div>
                    ))
                )}
                <div ref={postsEndRef} />
            </div>

            <div className="post-form">
                <h3>{editingPost ? 'Bejegyzés szerkesztése' : 'Írjon egy bejegyzést'}</h3>
                <textarea
                    value={newPost}
                    onChange={(e) => setNewPost(e.target.value)}
                    placeholder="Írjon valamit..."
                    maxLength={1000}
                />
                <button onClick={handlePost}>{editingPost ? 'Mentés' : 'Küldés'}</button>
            </div>
        </div>
    );
};

export default ChatPage;
