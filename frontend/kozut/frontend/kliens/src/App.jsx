import { useEffect, useState } from 'react'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'tachyons/css/tachyons.min.css'
import axios from 'axios'

function App() {
  const [data, setData] = useState([]);
  useEffect(() => {
    axios.get(`http://localhost:3001/regiok`)
        .then(data => setData(data.data))
        .catch(error => console.log("Hiba:" + error))
  }, []);
  return (
        <div className="container-fluid">
            <h1 className="text-center display-4 mt-5 mb-5">Régiók</h1>
            <table>
                <thead>
                    <tr>
                        <th>Rid</th>
                        <th>Régió</th>
                        <th>Régió típusa</th>
                    </tr>
                </thead>
                <tbody>
                {data.map(row => (
                    <tr key={row.Rid}>
                        <td>{row.Rid}</td>
                        <td>{row.regionev}</td>
                        <td>{row.regio_tipusa}</td>
                    </tr>
                ))}
                </tbody>
            </table>
            <div>
                <select name='regioSelect' id='regioSelector'>
                    {data.map(d => (
                        <option key={d.Rid} value={d.regionev}>{d.regionev}</option>
                    ))}
                </select>
                <button onClick='yes' type='submit'>Beküld</button>
            </div>
        </div>
  )
}

export default App
