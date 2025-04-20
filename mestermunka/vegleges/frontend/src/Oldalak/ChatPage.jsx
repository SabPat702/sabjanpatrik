import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/ChatPage.css';

const ChatPage = () => {
    const navigate = useNavigate();
    const [posts, setPosts] = useState([]);
    const [newTitle, setNewTitle] = useState('');
    const [newContent, setNewContent] = useState('');
    const [editMode, setEditMode] = useState(null);
    const [editTitle, setEditTitle] = useState('');
    const [editContent, setEditContent] = useState('');
    const [expandedPosts, setExpandedPosts] = useState([]);

    useEffect(() => {
        fetch('http://localhost:3001/chat')
            .then(res => res.json())
            .then(data => setPosts(data));
    }, []);

    const handleNewPost = () => {
        if (!newTitle || !newContent) return;

        fetch('http://localhost:3001/chat', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: newTitle, content: newContent}) 
        })
            .then(() => {
                setNewTitle('');
                setNewContent('');
                return fetch('http://localhost:3001/chat');
            })
            .then(res => res.json())
            .then(data => setPosts(data));
    };

    const handleDelete = (id) => {
        fetch(`http://localhost:3001/chat/${id}`, { method: 'DELETE' })
            .then(() => fetch('http://localhost:3001/chat'))
            .then(res => res.json())
            .then(data => setPosts(data));
    };

    const handleEdit = (id) => {
        fetch(`http://localhost:3001/chat/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: editTitle, content: editContent })
        })
            .then(() => {
                setEditMode(null);
                setEditContent('');
                setEditTitle('');
                return fetch('http://localhost:3001/chat');
            })
            .then(res => res.json())
            .then(data => setPosts(data));
    };

    const togglePost = (id) => {
        setExpandedPosts(prev =>
            prev.includes(id) ? prev.filter(pid => pid !== id) : [...prev, id]
        );
    };

    return (
        <div className="chat-page">
            <div className="cointainer-header">
                <nav className="chat-nav">
                    <span className="nav-title" onClick={() => navigate('/DungeonValleyExplorer')}>
                        Dungeon Valley Explorer
                    </span>
                    <span className="nav-center">💬 Chat Szoba</span>
                </nav>
            </div>
            <br />
            <div className="cointainer-posts">
                {posts.map(post => (
                    <div
                        key={post.id}
                        className={`post ${expandedPosts.includes(post.id) ? 'expanded' : ''}`}
                        onClick={() => togglePost(post.id)}
                    >
                        <div className="post-header">
                            {editMode === post.id ? (
                                <input
                                    type="text"
                                    value={editTitle ?? ''}
                                    onChange={(e) => setEditTitle(e.target.value)}
                                    placeholder="Edit title"
                                    className="edit-title-input"
                                />
                            ) : (
                                <span className="post-title">{post.Title}</span>
                            )}
                            <button
                                onClick={(e) => {
                                    e.stopPropagation();
                                    handleDelete(post.id);
                                }}
                                className="delete-btn"
                            >
                                🗑️
                            </button>
                        </div>

                        {expandedPosts.includes(post.id) && (
                            <>
                                {editMode === post.id ? (
                                    <div className="edit-post" onClick={e => e.stopPropagation()}>
                                        <textarea
                                            value={editContent}
                                            onChange={(e) => setEditContent(e.target.value)}
                                            placeholder="Edit your message"
                                        />
                                        <button onClick={() => handleEdit(post.id)} className="edit-btn">Mentés</button>
                                        <button onClick={() => setEditMode(null)} className="cancel-btn">Mégse</button>
                                    </div>
                                ) : (
                                    <div className="post-expanded-content">
                                        <p className="post-text">{post.Content}</p>
                                        <button
                                            onClick={(e) => {
                                                e.stopPropagation();
                                                setEditMode(post.id);
                                                setEditContent(post.Content);
                                                setEditTitle(post.title); // Set the title for edit mode
                                            }}
                                            className="edit-btn"
                                        >
                                            Szerkesztés
                                        </button>
                                    </div>
                                )}
                            </>
                        )}
                    </div>
                ))}
            </div>
            <br />
            <div className="cointainer-submit">
                <input
                    type="text"
                    value={newTitle}
                    onChange={(e) => setNewTitle(e.target.value)}
                    placeholder="Cím / Téma"
                    className="input-title"
                />
                <textarea
                    value={newContent}
                    onChange={(e) => setNewContent(e.target.value)}
                    placeholder="Írd ide az üzeneted..."
                    className="input-text"
                />
                <button onClick={handleNewPost} className="submit-btn">Küldés</button>
            </div>
        </div>
    );
};

export default ChatPage;