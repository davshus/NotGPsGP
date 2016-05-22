import java.awt.BorderLayout;
import java.awt.EventQueue;
import java.util.Random;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import jsSyntax.console;

import java.awt.FlowLayout;
import javax.swing.JLabel;
import javax.swing.JTextField;
import javax.swing.JButton;
import java.awt.Toolkit;
import javax.swing.ImageIcon;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

public class GeneHubGUI extends JFrame {

	private JPanel contentPane;
	private JTextField nGenerationInput;
	private JTextField popSizeInput;
	private Creature[] creatures;
	double[] weights = new double[16];
	Random rand = new Random(System.currentTimeMillis());
	String[] geneIdentifiers = new String[]{
			"SH",
			"RO",
			"ST",
			"MF"
			};
	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					GeneHubGUI frame = new GeneHubGUI();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public GeneHubGUI() {
		setIconImage(Toolkit.getDefaultToolkit().getImage(GeneHubGUI.class.getResource("/imgs/GeneHub Icon.png")));
		setTitle("GeneHub by David Shustin - Made for hackMHS II");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 763, 771);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JLabel nGeneration = new JLabel("Generation #:");
		nGeneration.setBounds(25, 13, 100, 20);
		contentPane.add(nGeneration);
		
		nGenerationInput = new JTextField();
		nGenerationInput.setBounds(130, 10, 356, 26);
		contentPane.add(nGenerationInput);
		nGenerationInput.setColumns(25);
		
		JLabel popSize = new JLabel("Population Size:");
		popSize.setBounds(25, 49, 114, 20);
		contentPane.add(popSize);
		
		popSizeInput = new JTextField();
		popSizeInput.setBounds(140, 46, 356, 26);
		contentPane.add(popSizeInput);
		popSizeInput.setColumns(25);
		
		JButton gnrteRandPop = new JButton("Generate Random Population");
		gnrteRandPop.setBounds(259, 85, 239, 29);
		contentPane.add(gnrteRandPop);
		
		JButton gnrteChildren = new JButton("Generate Children");
		
		gnrteChildren.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				creatures = new Creature[Integer.parseInt(popSizeInput.getText())];
				for (int i = 0; i < Double.parseDouble(popSizeInput.getText()); i++) {
					for (int j = 0; j < 16; j++) {
						weights[j] = rand.nextInt(20000)/10000.0 - 1;
						console.log(j);
					}
					creatures[i] = new Creature(4, 4, i, 0, weights, geneIdentifiers);
					creatures[i].generateFile();
					console.log(i);
				}
				console.log(Integer.parseInt(popSizeInput.getText()));
			}
		});
		gnrteChildren.setBounds(25, 85, 159, 29);
		contentPane.add(gnrteChildren);
		
		JLabel logo = new JLabel("");
		logo.setIcon(new ImageIcon(GeneHubGUI.class.getResource("/imgs/GeneHub Icon (Small).png")));
		logo.setBounds(15, 130, 600, 587);
		contentPane.add(logo);
	}
}
