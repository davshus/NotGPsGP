import java.util.Random;

import jsSyntax.console;

public class testtest {
	static String[] geneIdentifiers = new String[]{
	"SH",
	"RO",
	"ST",
	"MF"
	};
	static Random rand = new Random(System.currentTimeMillis());
	public static void main(String[] args) {
		double[] weights = new double[16];
		for (int i = 0; i < 16; i++) {
			weights[i] = rand.nextInt(10000)/10000.0;
		}
		Creature creature = new Creature(4, 4, 0, 0, weights, geneIdentifiers);
		creature.generateFile();
		console.log("test2");
	}

}
