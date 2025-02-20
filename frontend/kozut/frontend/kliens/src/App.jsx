import { useEffect, useState } from 'react'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'tachyons/css/tachyons.min.css'

function App() {
  const [felhasznalok, setFelhasznalok] = useState([]);
  useEffect(() => {
    fetch(`http://localhost:3001/regiok`)
        .then(res => res.json())
        .then(data => setFelhasznalok(data))
  }, []);
  return (
      <div className="container-fluid bg-light-gray">
            <article className="row justify-content-center ">
                <h1 className="text-center display-4 mt-5 mb-5 bg-light-blue">Régiók</h1>
                <div className="row">
                        {felhasznalok.map(felhasznalo => (
                            <div className="col-md-4">
                                <div className=" bg-light-red br3 pa3 ma2 grow bw2 shadow-5" key={felhasznalo.Rid}>
                                    <h2 className="text-center">Régió:</h2>
                                    <p>Régió neve: {felhasznalo.regionev}</p>
                                    <p>Régió típusa: {felhasznalo.regio_tipusa}</p>
                                </div>
                            </div>
                        ))}
                </div>
            </article>
        </div>
  )
}

export default App
