import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;

import jsSyntax.console;
public class Creature {
	double[] weights;
	int species;
	String filepath;
	Genome genome;
	int generation;
	int nInput, nOutput;
	public Creature (int xnInput, int xnOutput, int xSpecies, int xGeneration, double[] xWeights, String[] identifiers) {
		this.species = xSpecies;
		this.nInput = xnInput;
		this.nOutput = xnOutput;
		this.generation = xGeneration;
		weights = xWeights;
		this.genome = new Genome(xWeights, xnOutput, identifiers);
	}
	public void generateFile () {
		PrintWriter writer;
		File pathname = new File("generation_" + this.generation + "_species_" + this.species + ".txt");
		try {
			writer = new PrintWriter(pathname, "UTF-8");
			for (int j = 0; j < this.nOutput; j++) {
				writer.println(genome.ER[j]);
				writer.println(genome.BP[j]);
				writer.println(genome.BR[j]);
				writer.println(genome.LE[j]);
			}
			writer.close();
			console.log("test");
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
	}
}