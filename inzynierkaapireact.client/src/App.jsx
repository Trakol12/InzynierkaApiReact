import { useState } from 'react';
import './App.css';


function App() {
    const [eanCode, setEanCode] = useState('');
    const [castoCode, setCastoCode] = useState('');
    const [planogramId, setPlanogramId] = useState(null);
    const [productPageLink, setProductPageLink] = useState(null);
    const [imageUrl, setImageUrl] = useState(null);
    const [description, setDescription] = useState('');
    const [product, setProduct] = useState(null);
    const [error, setError] = useState('');
    const [registrationError, setRegistrationError] = useState(''); 
    const [showDownloadButton, setShowDownloadButton] = useState(false);
    const [showScrapeButton, setShowScrapeButton] = useState(false);
    const [userName, setUserName] = useState('');
    const [newUserName, setNewUserName] = useState('');
    const [password, setPassword] = useState('');
    const [isAdmin, setIsAdmin] = useState(false);
    const [adminPermision, setAdminPermision] = useState(false);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [showUserRegistration, setShowUserRegistration] = useState(false);
    const toggleUserRegistration = () => {
    setShowUserRegistration(prev => !prev);
    };


    const handleEanSearch = async () => {

        try {
            const response = await fetch(`http://localhost:5167/api/Product/search/ean/${eanCode}`);
            const data = await response.json();
            if (!response.ok) throw new Error(data.message); //Wypisuje wiadomośc zwracaną przez serwer                     
            console.log(data); // To pokaże wszystkie dostępne pola w konsoli przeglądarki
            setProduct(data);
            setError('');
            setShowDownloadButton(true);  // Pokaż przycisk pobierania
            setShowScrapeButton(true);
            setPlanogramId(data.planogramId); //Przypisanie wartości do planogramId po wyszukaniu produktu
            setProductPageLink(data.productPageLink); //Przypisanie wartości do planogramId po wyszukaniu produktu
            setDescription('');
            setImageUrl(null);

        } catch (err) {
            setError(err.message);
            setProduct(null);
            setShowDownloadButton(false); // Ukryj przycisk, jeśli wystąpi błąd
            setShowScrapeButton(false);
            setPlanogramId(null); //Czyszczenie planogramId jeśli wystąpił błąd przy pobieraniu produktu
            setProductPageLink(null); 
            setDescription('');
            setImageUrl(null);
        }
    };

    const handleCastoSearch = async () => {

        try {
            const response = await fetch(`http://localhost:5167/api/Product/search/Casto/${castoCode}`);
            const data = await response.json();
            if (!response.ok) throw new Error(data.message); //Wypisuje wiadomośc zwracaną przez serwer          
            console.log(data);
            setProduct(data);
            setError('');
            setShowDownloadButton(true);  // Pokaż przycisk pobierania
            setShowScrapeButton(true);
            setPlanogramId(data.planogramId); //Przypisanie wartości do planogramId po wyszukaniu produktu
            setProductPageLink(data.productPageLink); 
            setDescription('');
            setImageUrl(null);
        } catch (err) {
            setError(err.message);
            setProduct(null);
            setShowDownloadButton(false); // Ukryj przycisk, jeśli wystąpi błąd
            setShowScrapeButton(false);
            setPlanogramId(null); //Czyszczenie planogramId jeśli wystąpił błąd przy pobieraniu produktu
            setProductPageLink(null); 
            setDescription('');
            setImageUrl(null);
        }
    };

    const handlePlanogramDownload = async () => {
        try {        
            const url = `http://localhost:5167/api/Product/download/${planogramId}`;
            window.open(url, '_blank');
            setError(''); // Wyczyść błędy, jeśli operacja się powiodła
        } catch (err) {
            setError(err.message); // Ustaw błąd, jeśli coś poszło nie tak
            console.error("Error downloading file: ", err);
        }
    };

    const handleScraping = async () => {      
        try {
            const encodedUrl = encodeURIComponent(productPageLink);
            const response = await fetch(`http://localhost:5167/api/Product/scrape/${encodedUrl}`);
            const data = await response.json();
            setImageUrl(data.imageUrl);
            setDescription(data.description);
            setError(''); 
        }
        catch (err) {
            setError(err.message); // Ustaw błąd, jeśli coś poszło nie tak
            console.error("Error scraping: ", err);
        }
    }
    const handleLogin = async (userName, password) => {
        try {
            const response = await fetch('http://localhost:5167/api/Users/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ userName, password }),
            });

            const data = await response.json();
            console.log(data);
            if (response.ok) {
                console.log("Zalogowano pomyślnie");
                console.log("czy admin", data.isAdmin);
                setIsLoggedIn(true);
                setIsAdmin(data.isAdmin)
                setPassword('');
                setError('');
                
                
            } else {
                setError(data.message);
            }
        } catch (err) {
            setError('Błąd logowania');
            console.error(err);
        }
    };

    const handleRegister = async (userName, password, isAdmin) => {
    try {
        const response = await fetch('http://localhost:5167/api/Users/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ userName, password, isAdmin }),
        });

        const data = await response.json();
        if (response.ok) {
            console.log("Zarejestrowano pomyślnie");
            setRegistrationError("Zarejestrowano pomyślnie!");
        } else {
            setRegistrationError(data.message);
        }
    } catch (err) {
        setRegistrationError('Błąd rejestracji');
        console.error(err);
    }
    };
    const handleLogout = async () => {
        try {
            const response = await fetch('http://localhost:5167/api/Users/logout', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
            });

            if (response.ok) {
                console.log("Wylogowano pomyślnie");
                setIsLoggedIn(false); // Wylogowanie użytkownika w React
                setIsAdmin(false); // Resetowanie uprawnień admina
                setUserName('');
                setPassword('');
                setEanCode(''); // Czyszczenie kodu EAN
                setCastoCode(''); // Czyszczenie kodu Casto
                setPlanogramId(null); // Resetowanie ID planogramu
                setProductPageLink(null); // Czyszczenie linku do strony produktu
                setImageUrl(null); // Czyszczenie URL zdjęcia
                setDescription(''); // Czyszczenie opisu produktu
                setProduct(null); // Czyszczenie szczegółów produktu
                setNewUserName('');
                setError(''); // Czyszczenie błędów
                setRegistrationError('');
                setShowDownloadButton(false); // Ukrycie przycisku pobierania
                setShowScrapeButton(false); // Ukrycie przycisku scrapowania
                setShowUserRegistration(false);
            } else {
                console.error("Błąd podczas wylogowywania");
                setError("Błąd podczas wylogowywania.");
            }
        } catch (err) {
            console.error("Błąd połączenia z serwerem:", err);
            setError("Błąd połączenia z serwerem.");
        }
    }

    return (
        <div className="App">
            {isLoggedIn && (
                <header className="header">
                    Witaj, {isLoggedIn ? userName : 'Użytkowniku'}!
                    {isLoggedIn && (
                        <button onClick={handleLogout} className="logout-button">Wyloguj</button>
                    )}
                    {isAdmin ? (
                        <p className="admin-message">Jesteś zalogowany jako administrator.</p>
                    ) : (
                        <p className="user-message">Jesteś zalogowany jako zwykły użytkownik.</p>
                    )}
                    <div className="spacer"></div>
                    <div className="registration-button-container">    
                        {isAdmin && (
                            <button onClick={toggleUserRegistration} className="button">Zarejestruj nowego użytkownika</button>
                        )}
                    </div>
                </header>
            )}
            <main className="main-content">
            {!isLoggedIn ? (
                <div className="login-container">
                    <h2>Logowanie</h2>
                    <input
                        type="text"
                        value={userName}
                        onChange={e => setUserName(e.target.value)}
                    placeholder="Nazwa użytkownika"
                    className="input-field"
            />
                    <input
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        placeholder="Hasło"
                        className="input-field"
                    />
                    <button onClick={() => handleLogin(userName, password)} className="button">Zaloguj</button>
                    {error && <p className="error-message">{error}</p>}
                </div>
            ) : (
                <div className="dashboard-container">
                    

            {/* User Registration Form */}
            {showUserRegistration && (
            <div className="registration-container">
                <h2>Rejestracja nowego użytkownika</h2>
                <input
                    type="text"
                    value={newUserName}
                onChange={e => setNewUserName(e.target.value)}
                placeholder="Nazwa użytkownika"
                className="input-field"
                    />
                <input
                    type="password"
                    value={password}
                onChange={e => setPassword(e.target.value)}
                placeholder="Hasło"
                className="input-field"
                    />
                <label>
                    <input
                        type="checkbox"
                        checked={adminPermision}
                        onChange={e => setAdminPermision(e.target.checked)}
                    />
                    Administrator
                </label>
                <button onClick={() => handleRegister(newUserName, password, adminPermision)} className="button">Zarejestruj</button>
                {registrationError && (
                    <p className="error-message" style={{ color: registrationError.includes("pomyślnie") ? 'green' : 'red' }}>
                        {registrationError}
                    </p>
                )}
            </div>
            )}

            {/* Wyszukiwanie produktów */}
            <h2>Wyszukiwanie Produktów</h2>
            <div className="search-container">
                <input
                    type="text"
                    value={eanCode}
                    onChange={e => setEanCode(e.target.value)}
                    placeholder="Wpisz kod EAN"
                    className="input-field"
                />
                <button onClick={handleEanSearch} className="button">Szukaj po EAN</button>
                                
            </div>
            <div className="search-container">
                <input
                    type="text"
                    value={castoCode}
                    onChange={e => setCastoCode(e.target.value)}
                    placeholder="Wpisz kod Casto"
                    className="input-field"
                />
                <button onClick={handleCastoSearch} className="button">Szukaj po Casto</button>
                               
            </div>
            {error && (
                    <p className="error-message" style={{ color: error.includes("pomyślnie") ? 'green' : 'red' }}>
                        {error}
                    </p>
                )}
            {product && (
                <div className="product-results">
                    <h2>Wyniki:</h2>
                    <p>Kod Ean: {product.eanCode}</p>
                    <p>Nazwa produktu: {product.name}</p>
                    <p>Kod Casto: {product.castoCode}</p>
                    <p>Kategoria: {product.category}</p>
                    <p>Link do Produktu: <a href={product.productPageLink} target="_blank" rel="noopener noreferrer">{product.productPageLink}</a></p>
                    <p>Alejka: {product.alley}</p>
                    <p>Numer na półce: {product.numberOnTheShelf}   Numer na ekspozycji: {product.numerOnTheExposition}</p>                    
                    {showScrapeButton && (
                        <button onClick={handleScraping} className="button">Wyświetl zdjęcie oraz opis</button>
                                )}
                    {showDownloadButton && (
                        <button onClick={handlePlanogramDownload} className="button">Pobierz Planogram</button>
                                )}
                    
                
                </div>
            )}

            {/* Scraping zdjęcia i opisu */}
            
            <div className="scraping-container">
                
                {imageUrl && (
                    <div className="image-container">
                        <h2>Zdjęcie produktu:</h2>
                        <img src={imageUrl} alt="Zdjęcie produktu" className="product-image" />
                    </div>
                )}
                {description && (
                    <div className="description-container">
                        <h2>Opis produktu:</h2>
                        <p>{description}</p>
                    </div>
                )}
                                </div>

        </div>

    )

                }
            </main>
</div >

    );
}

export default App;