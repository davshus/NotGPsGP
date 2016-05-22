using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class NeuralNetworkB : MonoBehaviour {
	public GameObject enemy;
	public GameObject bullet;
	Rigidbody rb;
	int generation;
	int currentSpecies = 0;
	string line;
	public int speed;
	public int torqueVal;
	string settingName;
	string settingVal;
	int populationSize;
	Transform tr;
	int shoot;
	bool canShoot = true;
	int nHits = 0;
	GameObject[] myBullets;
	GameObject bulletInstance;
	float SH, RO, ST, MF;
	double ERSH, BPSH, BRSH, LESH, ERRO, BPRO, BRRO, LERO, ERST, BPST, BRST, LEST, ERMF, BPMF, BRMF, LEMF;
	IEnumerator genomeParser () {
		for (int currentSpecies = 0; currentSpecies < populationSize/2; currentSpecies++) {
			tr.position = new Vector3 (4f, -0.25f, 0f);
			tr.rotation = Quaternion.identity;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			StreamReader theReader = new StreamReader (Application.dataPath + "\\Creatures\\generation_" + generation + "_species_" + ((currentSpecies*2)+1) + ".txt", Encoding.Default);
			using (theReader) {
				do {
					line = theReader.ReadLine ();

					if (line != null) {
						switch (line.Substring (0, 4)) {
						case "ERSH":
							ERSH = Convert.ToDouble (line.Substring (5));
							Debug.Log(ERSH);
							break;
						case "BPSH":
							BPSH = Convert.ToDouble (line.Substring (5));
							Debug.Log(BPSH);
							break;
						case "BRSH":
							BRSH = Convert.ToDouble (line.Substring (5));
							Debug.Log(BRSH);
							break;
						case "LESH":
							LESH = Convert.ToDouble (line.Substring (5));
							Debug.Log(LESH);
							break;
						case "ERRO":
							ERRO = Convert.ToDouble (line.Substring (5));
							break;
						case "BPRO":
							BPRO = Convert.ToDouble (line.Substring (5));
							break;
						case "BRRO":
							BRRO = Convert.ToDouble (line.Substring (5));
							break;
						case "LERO":
							LERO = Convert.ToDouble (line.Substring (5));
							break;
						case "ERST":
							ERST = Convert.ToDouble (line.Substring (5));
							break;
						case "BPST":
							BPST = Convert.ToDouble (line.Substring (5));
							break;
						case "BRST":
							BRST = Convert.ToDouble (line.Substring (5));
							break;
						case "LEST":
							LEST = Convert.ToDouble (line.Substring (5));
							break;
						case "ERMF":
							ERMF = Convert.ToDouble (line.Substring (5));
							break;
						case "BPMF":
							BPMF = Convert.ToDouble (line.Substring (5));
							break;
						case "BRMF":
							BRMF = Convert.ToDouble (line.Substring (5));
							break;
						case "LEMF":
							LEMF = Convert.ToDouble (line.Substring (5));
							break;
						}
					}
				} while (line != null);  
				theReader.Close ();
			}
			Debug.Log ("come on");
			yield return new WaitForSeconds (10f);
			myBullets = GameObject.FindGameObjectsWithTag ("BulletB");
			foreach (GameObject singleBullet in myBullets) {
				GameObject.Destroy (singleBullet);
			}
		}
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		tr = GetComponent<Transform> ();
		StreamReader settingsReader = new StreamReader (Application.dataPath  + "\\settings.txt", Encoding.Default);
		using (settingsReader) {
			do {
				settingName = settingsReader.ReadLine();
				if (settingName != null) {
					settingVal = settingsReader.ReadLine();
					switch (settingName) {
					case "generation":
						generation = Convert.ToInt32(settingVal);
						break;
					case "populationSize":
						populationSize = Convert.ToInt32(settingVal);
						break;
					}
				}
			} while (settingName != null);
		}
		settingsReader.Close();
		StartCoroutine (genomeParser ());
	}
	int getER () {
		if (enemy.transform.position.x > tr.position.x) {
			return 1;
		} else {
			return -1;
		}
	}
	int getBP () {
		if (GameObject.Find ("BulletA") == null) {
			return 0;
		} else {
			return (int)(Vector3.Distance (GameObject.Find ("BulletA").transform.position, tr.position) / 15.0);
		}
	}
	int getBR () {
		if (GameObject.Find ("BulletA") == null) {
			return 0;
		} else if (GameObject.Find ("BulletA").transform.position.x > tr.position.x) {
			return 1;
		} else {
			return -1;
		}
	}
	int getLE () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			if (hit.transform.gameObject.CompareTag ("EnemyA")) {
				return 1;
			} else {
				return 0;
			}
		} else {
			return 0;
		}
	}
	IEnumerator waitToShoot () {
		canShoot = false;
		yield return new WaitForSeconds (2f);
		canShoot = true;
	}
	// Update is called once per frame
	void Update () {
		float shInpCalc = (float)((getER () * ERSH) + (getBP () * BPSH) + (getBR () * BRSH) + (getLE () * LESH)-0.5f);
		float roInpCalc = (float)((getER () * ERRO) + (getBP () * BPRO) + (getBR () * BRRO) + (getLE () * LERO));
		float stInpCalc = (float)((getER () * ERST) + (getBP () * BPST) + (getBR () * BRST) + (getLE () * LEST));
		float mfInpCalc = (float)((getER () * ERMF) + (getBP () * BPMF) + (getBR () * BRMF) + (getLE () * LEMF));
		SH = Mathf.Sign(shInpCalc);
		RO = Mathf.Sign(roInpCalc);
		//ST = Mathf.Sign(stInpCalc);
		MF = Mathf.Sign(mfInpCalc);
		//Shoot
		if (SH == 1) {
			if (canShoot == true) {

				bulletInstance = Instantiate (bullet, tr.transform.position+tr.transform.right, Quaternion.identity) as GameObject;
				bulletInstance.GetComponent<Rigidbody>().velocity = tr.right * 2;
				//canShoot = false;
				StartCoroutine(waitToShoot ());
			}
		}

		//Rotate
		if (RO == 1) {
			rb.AddTorque (transform.forward * torqueVal);
		} else if (RO == -1) {
			rb.AddTorque (transform.forward * -torqueVal);
		}
		//Strafe
		if (ST == 1) {
			rb.AddForce (transform.right * speed);
		} else if (RO == -1) {
			rb.AddForce (transform.right * -speed);
		}
		//Longitudal movement
		if (MF == 1) {
			rb.AddForce (transform.forward * speed);
		} else if (MF == -1) {
			rb.AddForce (transform.forward * -speed);
		}
	}



}
