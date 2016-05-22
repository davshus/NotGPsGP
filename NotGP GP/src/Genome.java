
public class Genome {
	public String[] ER;
	public String[] BP;
	public String[] BR;
	public String[] LE;
	public String[] identifier;
	public Genome (double[] xGenes, int nOutput, String[] geneIdentifiers) {
		this.identifier = geneIdentifiers;
		this.ER = new String[nOutput];
		this.BP = new String[nOutput];
		this.BR = new String[nOutput];
		this.LE = new String[nOutput];
		int n = 0;
		for (int i = 0; i < nOutput; i++) {
			ER[i] = "ER" + identifier[i] + "_" + Double.toString(xGenes[n++]);
			BP[i] = "BP" + identifier[i] + "_" + Double.toString(xGenes[n++]);
			BR[i] = "BR" + identifier[i] + "_" + Double.toString(xGenes[n++]);
			LE[i] = "LE" + identifier[i] + "_" + Double.toString(xGenes[n++]);
		}

	}
}
